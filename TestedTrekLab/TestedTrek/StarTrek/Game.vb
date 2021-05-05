Imports System.Collections.Generic
Imports StarTrek.Untouchables

Public Class Game

    Private e As Integer = 10000
    Private t As Integer = 8

    Public Function EnergyRemaining() As Integer
        Return e
    End Function

    Public Property Torpedoes() As Integer
        Get
            Return t
        End Get
        Set
            t = value
        End Set
    End Property

    Public Sub FireWeapon(wg As WebGadget)
        FireWeapon(New Galaxy(wg))
    End Sub

    Public Sub FireWeapon(wg As Galaxy)
        If wg.Parameter("command").Equals("phaser") Then
            Dim amount As Integer = Integer.Parse(wg.Parameter("amount"))
            Dim enemy As Klingon = DirectCast(wg.Variable("target"), Klingon)
            If e >= amount Then
                Dim distance As Integer = enemy.Distance()
                If distance > 4000 Then
                    wg.WriteLine("Klingon out of range of phasers at " & distance & " sectors...")
                Else
                    Dim damage As Integer = amount - (((amount \ 20) * distance \ 200) + Rnd(200))
                    If damage < 1 Then
                        damage = 1
                    End If
                    wg.WriteLine("Phasers hit Klingon at " & distance & " sectors with " & damage & " units")
                    If damage < enemy.GetEnergy() Then
                        enemy.SetEnergy(enemy.GetEnergy() - damage)
                        wg.WriteLine("Klingon has " & enemy.GetEnergy() & " remaining")
                    Else
                        wg.WriteLine("Klingon destroyed!")
                        enemy.Delete()
                    End If
                End If

                e -= amount
            Else
                wg.WriteLine("Insufficient energy to fire phasers!")

            End If
        ElseIf wg.Parameter("command").Equals("photon") Then
            Dim enemy As Klingon = DirectCast(wg.Variable("target"), Klingon)
            If t > 0 Then
                Dim distance As Integer = enemy.Distance()
                If (Rnd(4) + ((distance \ 500) + 1) > 7) Then
                    wg.WriteLine("Torpedo missed Klingon at " & distance & " sectors...")
                Else
                    Dim damage As Integer = 800 + Rnd(50)
                    wg.WriteLine("Photons hit Klingon at " & distance & " sectors with " & damage & " units")
                    If damage < enemy.GetEnergy() Then
                        enemy.SetEnergy(enemy.GetEnergy() - damage)
                        wg.WriteLine("Klingon has " & enemy.GetEnergy() & " remaining")
                    Else
                        wg.WriteLine("Klingon destroyed!")
                        enemy.Delete()
                    End If
                End If

                t -= 1
            Else
                wg.WriteLine("No more photon torpedoes!")
            End If
        End If
    End Sub


    ' note we made generator public in order to mock it
    ' it's ugly, but it's telling us something about our *design!* ;-)
    Public Shared generator As New Random()
    Private Shared Function Rnd(maximum As Integer) As Integer
        Return generator.[Next](maximum)
    End Function


End Class
