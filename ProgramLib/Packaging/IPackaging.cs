namespace ProgramLib.Packaging {
    public interface IPackaging {
        void Unpackaging(string path);
        void Packaging(string path);

        void Unpackaging(string pathSource, string pathDestination);
        void Packaging(string pathSource, string pathDestination);
    }
}