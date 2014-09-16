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
          HelpText = "Path of directory to save.")]
        public string OutputPath { get; set; }

        [Option('h', "html", Required = false, DefaultValue = false,
          HelpText = "If true, will build HTML pages instead of a PDF.")]
        public bool BuildHtml { get; set; }

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
