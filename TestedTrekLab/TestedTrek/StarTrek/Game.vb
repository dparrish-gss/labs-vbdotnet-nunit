Imports System.Collections.Generic
Imports StarTrek.Untouchables

Public Class Game

    Private phaserEnergyReserve As Integer = 10000
    Private torpedoCount As Integer = 8

    Public Function EnergyRemaining() As Integer
        Return phaserEnergyReserve
    End Function

    Public Property Torpedoes() As Integer
        Get
            Return torpedoCount
        End Get
        Set
            torpedoCount = Value
        End Set
    End Property

    Public Sub FireWeapon(wg As WebGadget)
        FireWeapon(New Galaxy(wg))
    End Sub

    Public Sub FireWeapon(wg As Galaxy)
        Dim enemy As Klingon = DirectCast(wg.Variable("target"), Klingon)
        Dim distance As Integer = 0
        If enemy IsNot Nothing Then distance = enemy.Distance()

        If wg.Parameter("command").Equals("phaser") Then
            Dim phaserEnergyPerShot As Integer = Integer.Parse(wg.Parameter("amount"))

            If phaserEnergyReserve >= phaserEnergyPerShot Then
                Dim maxPhaserDistance = 4000

                If distance > maxPhaserDistance Then
                    wg.WriteLine("Klingon out of range of phasers at " & distance & " sectors...")
                Else
                    Dim damage As Integer = CalculatePhaserDamage(phaserEnergyPerShot, distance)
                    wg.WriteLine("Phasers hit Klingon at " & distance & " sectors with " & damage & " units")

                    DamageOrDestroyEnemy(wg, enemy, damage)
                End If

                phaserEnergyReserve -= phaserEnergyPerShot
            Else
                wg.WriteLine("Insufficient energy to fire phasers!")
            End If
        ElseIf wg.Parameter("command").Equals("photon") Then
            If torpedoCount > 0 Then
                If DoesProjectileMiss(distance) Then
                    wg.WriteLine("Torpedo missed Klingon at " & distance & " sectors...")
                Else
                    Dim damage As Integer = CalculatePhotonDamage()
                    wg.WriteLine("Photons hit Klingon at " & distance & " sectors with " & damage & " units")

                    DamageOrDestroyEnemy(wg, enemy, damage)
                End If

                torpedoCount -= 1
            Else
                wg.WriteLine("No more photon torpedoes!")
            End If
        End If
    End Sub

    Public Function CalculatePhaserDamage(phaserEnergyPerShot As Integer, distance As Integer) As Integer
        Dim damage As Integer = phaserEnergyPerShot - (((phaserEnergyPerShot \ 20) * distance \ 200) + Rnd(200))
        If damage < 1 Then damage = 1
        Return damage
    End Function

    Public Function CalculatePhotonDamage() As Integer
        Return 800 + Rnd(50)
    End Function

    Public Function DoesProjectileMiss(distance As Integer) As Boolean
        Return Rnd(4) + ((distance \ 500) + 1) > 7
    End Function

    Public Sub DamageOrDestroyEnemy(wg As Object, enemy As Object, damage As Integer)
        If damage < enemy.GetEnergy() Then
            enemy.TakeDamage(damage)
            wg.WriteLine("Klingon has " & enemy.GetEnergy() & " remaining")
        Else
            wg.WriteLine("Klingon destroyed!")
            enemy.Delete()
        End If
    End Sub

    ' note we made generator public in order to mock it
    ' it's ugly, but it's telling us something about our *design!* ;-)
    Public Shared generator As New Random()
    Private Shared Function Rnd(maximum As Integer) As Integer
        Return generator.[Next](maximum)
    End Function


End Class
