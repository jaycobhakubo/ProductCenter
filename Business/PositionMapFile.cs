using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTI.Modules.ProductCenter.Business
{
    public class PositionMapFile : IPositionMap
    {
        public byte MapSpace { get; private set; }

        public int NumSequences { get; private set; }

        public PositionSequence GetSequence(int setNumber)
        {
            if(setNumber > NumSequences)
                return null;

            System.IO.FileStream fs = null;
            fs = System.IO.File.OpenRead(FileName);

            fs.Position = VersionIdStringLength + 32 + 5 + MapSpace * setNumber;
            var sourceBytes = new byte[MapSpace];
            fs.Read(sourceBytes, 0, MapSpace);
            fs.Close();

            var setBytes = new byte?[MapSpace];
            for(int i = 0; i < MapSpace; ++i)
                if(sourceBytes[i] < MapSpace)
                    setBytes[i] = sourceBytes[i];
                else
                    setBytes[i] = null;

            return new PositionSequence(setBytes);
        }

        public List<PositionSequence> GetSequences(int firstSetNumber, int seqCount)
        {
            var sets = new List<PositionSequence>();

            System.IO.FileStream fs = null;
            fs = System.IO.File.OpenRead(FileName);

            if(firstSetNumber + seqCount >= NumSequences)
                seqCount = NumSequences - firstSetNumber;

            for(int s = 0; s < seqCount; ++s)
            {
                fs.Position = VersionIdStringLength + 32 + 5 + MapSpace * (firstSetNumber + s);
                var sourceBytes = new byte[MapSpace];
                fs.Read(sourceBytes, 0, MapSpace);

                var setBytes = new byte?[MapSpace];
                for(int i = 0; i < MapSpace; ++i)
                    if(sourceBytes[i] < MapSpace)
                        setBytes[i] = sourceBytes[i];
                    else
                        setBytes[i] = null;
                sets.Add(new PositionSequence(setBytes));
            }
            fs.Close();

            return sets;
        }

        public int Version { get; private set; }
        public Guid MapId { get; private set; }
        public string FileName { get; private set; }

        #region Static Implementation
        public const int VersionIdStringLength = 32;
        private static readonly string[] VersionStrings = new string[] { "6958AB5105EF46C2B409CDC938CF12D8" };
        public const string StandardExtension = "pmf";

        public static int FileVersion(string versionIdStr)
        {
            for(int i = 0; i < VersionStrings.Length; ++i)
                if(versionIdStr == VersionStrings[i])
                    return i + 1;

            return 0;
        }

        public static PositionMapFile FromFile(string fileName, out string error)
        {
            PositionMapFile loadedMap = null;
            System.IO.FileStream fs = null;
            System.IO.BinaryReader br = null;
            error = null;

            try
            {
                fs = System.IO.File.OpenRead(fileName);
                br = new System.IO.BinaryReader(fs);
                if(fs.Length < VersionIdStringLength)
                    throw new FormatException("Ill-formed or invalid file.");

                var versionIdStr = new String(br.ReadChars(VersionIdStringLength));
                var mapIdStr = new String(br.ReadChars(32));

                var versionNum = FileVersion(versionIdStr);

                if(versionNum == 0)
                    throw new FormatException("File invalid or of an unrecognized format version.");

                if(versionNum == 1)
                {
                    var mapSpace = br.ReadByte();
                    var setCount = br.ReadInt32();

                    if(fs.Length < VersionIdStringLength + 5 + mapSpace * setCount)
                        throw new FormatException("File not large enough to contain what it claims.");

                    loadedMap = new PositionMapFile()
                    {
                        FileName = fileName,
                        MapId = new Guid(mapIdStr),
                        Version = versionNum,
                        NumSequences = setCount,
                        MapSpace = mapSpace
                    }
                    ;
                }
                else
                    throw new FormatException(String.Format("File version {0} not supported", versionNum));

            }
            catch(Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if(br != null)
                    br.Close();
            }

            return loadedMap;
        }

        public static bool ToFile(IPositionMap pm, string targetFileName)
        {
            return ToFile(pm, targetFileName, Guid.Empty);
        }

        public static bool ToFile(IPositionMap pm, string targetFileName, Guid mapId)
        {
            System.IO.FileStream fs = null;
            System.IO.BinaryWriter bw = null;

            try
            {
                if(mapId.Equals(Guid.Empty))
                {
                    var mf = pm as PositionMapFile;
                    mapId = (mf == null) ? Guid.NewGuid() : mf.MapId;
                }
                var mapIdStr = mapId.ToString("N");

                int formatVersion = 1; //In the future, could be dynamically determined by properties of pm

                fs = System.IO.File.OpenWrite(targetFileName);
                bw = new System.IO.BinaryWriter(fs);

                var e = new System.Text.ASCIIEncoding();

                bw.Write(e.GetBytes(VersionStrings[formatVersion - 1]));
                bw.Write(e.GetBytes(mapIdStr));

                if(formatVersion == 1)
                {
                    bw.Write(pm.MapSpace);
                    bw.Write(pm.NumSequences);
                    for(int set_i = 0; set_i < pm.NumSequences; ++set_i)
                    {
                        var set = pm.GetSequence(set_i).GetNormalizedSequence();
                        if(set == null)
                            throw new InvalidOperationException(String.Format("Failed to get normalized sequence for set {0}", set_i));
                        bw.Write(set);
                    }
                    bw.Close();
                    return true;
                }
                else
                    throw new NotSupportedException("PositionMap pm requires unsupported file format.");

            }
            catch(Exception ex)
            {
                if(bw != null)
                    bw.Close();
                if(fs != null)
                    System.IO.File.Delete(targetFileName);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }

        }
        #endregion

    }
}
