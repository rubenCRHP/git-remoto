Imports System
Imports System.IO

Public Class CListaClientes
  Private listaClientes As String()
  Private nElementos As Integer

  Public Sub New(ByVal n As Integer)
    nElementos = n
    listaClientes = New String(n - 1) {}
  End Sub

  Public Sub Añadir(ByVal nombre As String, ByVal i As Integer)
    listaClientes(i) = nombre & Environment.NewLine
  End Sub

  Public Sub Escribir(ByVal fcli As StreamWriter)
    For i As Integer = 0 To listaClientes.Length - 1
      fcli.Write(listaClientes(i))
    Next i
  End Sub
End Class
