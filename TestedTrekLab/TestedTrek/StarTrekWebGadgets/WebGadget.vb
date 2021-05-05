Imports System.Collections.Generic

Namespace Untouchables

    Public NotInheritable Class WebGadget

        Private commandParameter As String
        Private commandArgument As String
        Private targetVariable As Object

        Public Sub New(commandParameter As String, commandArgument As String, targetVariable As Object)
            Me.commandParameter = commandParameter
            Me.commandArgument = commandArgument
            Me.targetVariable = targetVariable
        End Sub

        Public Function Parameter(parameterName As String) As String
            If parameterName.Equals("command") Then
                Return commandParameter
            Else
                Return commandArgument
            End If
        End Function

        Public Function Variable(variableName As String) As Object
            Return targetVariable
        End Function

        Public Sub WriteLine(message As String)
            Console.WriteLine(message)
        End Sub

    End Class
End Namespace
