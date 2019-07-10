using System.Collections.Generic;

namespace Panviva.RebootDemoBot.App.Model
{
    public class Artefacts
    {
        public List<Result> Results { get; set; }
        public int Total { get; set; }
    }

    public class Content
    {
        public string MediaType { get; set; }
        public string Text { get; set; }
    }

    public class Result
    {
        public int Id { get; set; }
        public List<Content> Content { get; set; }
    }
}
