Imports System.Collections.Generic
Imports System.Collections

Imports System.Text

Public Class MockGalaxy
    Inherits Galaxy
    Private stuff As New Hashtable()
    Private buffer As New StringBuilder()
    Public Sub New()
        MyBase.New(Nothing)
    End Sub

    Public Overrides Function Parameter(parameterName As String) As String
        Return DirectCast(stuff(parameterName), String)
    End Function

    Public Overrides Function Variable(variableName As String) As Object
        Return stuff(variableName)
    End Function

    Public Overrides Sub WriteLine(message As String)
        Const  fakeNewLine As String = " || "
        buffer.Append(message & fakeNewLine)
    End Sub

    Friend Sub SetValueForTesting(key As String, value As Object)
        stuff(key) = value
    End Sub

    Friend Function GetAllOutput() As String
        Return buffer.ToString()
    End Function
End Class
