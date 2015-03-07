using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace downloadInfiniteStory
{
    class CommandLineOptions
    {
        [Option('r', "room", Required = true,
          HelpText = "Id of starting room.")]
        public string RoomId { get; set; }

        [Option('p', "path", Required = true,
          HelpText = "Path of directory or filename to save.")]
        public string OutputPath { get; set; }

        [Option('h', "html", Required = false, DefaultValue = false,
          HelpText = "Set program to export HTML pages.")]
        public bool BuildHtml { get; set; }

        [Option('e', "epub", Required = false, DefaultValue = false,
          HelpText = "Set program to export EPUB.")]
        public bool BuildEpub { get; set; }

        [Option('d', "pdf", Required = false, DefaultValue = false,
          HelpText = "Set program to export PDF.")]
        public bool BuildPdf { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
