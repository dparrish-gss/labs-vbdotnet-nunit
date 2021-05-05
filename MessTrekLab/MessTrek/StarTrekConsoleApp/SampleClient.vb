Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Untouchables
    ''' <summary>
    ''' Note:  SampleClient is UNTOUCHABLE!
    ''' It represents one of hundreds of Game clients, and should not have to change.
    ''' </summary>
    Public Class SampleClient
        Public Shared Sub Main()
            Console.WriteLine("Simple Star Trek")
            Dim wg As New WebGadget("phaser", "1000", New Klingon())
            Dim game As New Game()
            game.FireWeapon(wg)
            Console.ReadLine()
        End Sub
    End Class
End Namespace
