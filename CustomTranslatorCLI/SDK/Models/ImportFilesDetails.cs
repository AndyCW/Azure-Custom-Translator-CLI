namespace CustomTranslator.Models
{
    public class Rootobject
    {
        public ImportFilesDetails[] Content { get; set; }
    }

    public class ImportFilesDetails
    {
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public FileDetail[] FileDetails { get; set; }
    }

    public class FileDetail
    {
        public string Name { get; set; }
        public string LanguageCode { get; set; }
        public bool OverwriteIfExists { get; set; }
    }
}
