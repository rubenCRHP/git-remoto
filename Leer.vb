Imports System

Namespace MisClases.ES ' espacio de nombres
  Public Class Leer ' clase Leer
    Public Shared Function datoShort() As Short
      Try
        Return Int16.Parse(Console.ReadLine())
      Catch e1 As FormatException
        Return Int16.MinValue ' valor más pequeño
      End Try
    End Function

    Public Shared Function datoInt() As Integer
      Try
        Return Int32.Parse(Console.ReadLine())
      Catch e1 As FormatException
        Return Int32.MinValue ' valor más pequeño
      End Try
    End Function

    Public Shared Function datoLong() As Long
      Try
        Return Int64.Parse(Console.ReadLine())
      Catch e1 As FormatException
        Return Int64.MinValue ' valor más pequeño
      End Try
    End Function

    Public Shared Function datoFloat() As Single
      Try
        Return Single.Parse(Console.ReadLine())
      Catch e1 As FormatException
        Return Single.NaN ' No es un Número; valor float.
      End Try
    End Function

    Public Shared Function datoDouble() As Double
      Try
        Return Double.Parse(Console.ReadLine())
      Catch e1 As FormatException
        Return Double.NaN ' No es un Número; valor double.
      End Try
    End Function
  End Class
End Namespace
