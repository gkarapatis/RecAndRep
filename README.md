# RecAndRep

This is an application that performs pre-recorded actions on behalf of the user. It consists of two WPF applications one for the client and one for the server.
The server-side application hosts a SignalR server via OWIN on which both applications are connected during initialization. The SignalR is the communication channel between the two applications.

The server window contains some configured action scripts that the user can select (or add new ones if needed) Multiple actions can be selected by ctrl-click.
Then it instructs the connected client to start executing the actions detailed in the action script and records back the results (success/failure).

## Extensibility

The client application can be extended by adding dlls containing new actions.
These actions bust have the following structure:

```
[Operator("OperatorName")]
public class OperatorName
{
	[Action("ActionName")]
	public ActionResponse ActionName(string input1, string input2)
	{
		return new ActionResponse()
		{
			Succeeded = true,
		};
	}
}

```
The action can be executed by the server with the following script
```
OperatorName-ActionName input1;input2
```

## Frameworks - technologies used

- WPF
- OWIN
- SignalR (server and client)
- Log4Net
- Simplify.Windows.Forms
- LiteDB
- xUnit

### Prerequisites

Visual Studio

