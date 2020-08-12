using System.Collections.Generic;

namespace CustomTranslator.Models
{
    public class Rootobject
    {
        public DocumentDetailsForImportRequest[] Content { get; set; }
    }

    public class DocumentDetailsForImportRequest
    {
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public List<FileForImportRequest> FileDetails { get; set; }
        public bool IsParallel { get; set; }
    }

    public class FileForImportRequest
    {
        public string Name { get; set; }
        public string LanguageCode { get; set; }
        public bool OverwriteIfExists { get; set; }
    }
}
