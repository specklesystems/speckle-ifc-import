using System.CommandLine;
using Speckle.WebIfc.Importer;

var filePathArgument = new Argument<string>
  (name: "filePath");
var userIdArgument = new Argument<string>
  ("userId");
var streamIdArgument = new Argument<string>
  ("streamId");
var branchNameArgument = new Argument<string>
  ("branchName");
var commitMessageArgument = new Argument<string>
  ("commitMessage");
var tokenArgument = new Argument<string>
  ("token");

var rootCommand = new RootCommand
{
  filePathArgument,
  userIdArgument,
  streamIdArgument,
  branchNameArgument,
  commitMessageArgument,
  tokenArgument
};
rootCommand.SetHandler(async (filePath, userId, streamId, branchName, commitMessage, token) =>
{
  await Import.Ifc(filePath, userId, streamId, branchName, commitMessage, token);
}, filePathArgument, userIdArgument, streamIdArgument, branchNameArgument, commitMessageArgument, tokenArgument);
await rootCommand.InvokeAsync(args);
