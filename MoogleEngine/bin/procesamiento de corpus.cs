
using System.Reflection;
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Runtime.InteropServices.ComTypes;

using System.ComponentModel;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;


namespace Name
{

   class moggle
 {
 
  
 
   public static string [] ReadFolder()//escanear carpetas
  {
    string[] filas = Directory.GetFiles(@"C:\Users\fosythe´s family\Desktop\escanear ,leer y guardar\Content", "*.txt", SearchOption.AllDirectories);
   
    return filas ;
  }
    public static string [] ReadFiles()
  {
    Console.WriteLine("leyendo documentos....");

    string [] files = ReadFolder();
  
    string [] contenedor = new string[files.Length];


      for (int i = 0; i < files.Length; i++)
      {
         string contenido = " ";
         //leer archivos
         StreamReader reader = new StreamReader(files[i]);

         while (!reader.EndOfStream)
         {
            //guardo el contenido del archivo
            contenido += reader.ReadLine() + " ";
         }
            contenedor[i] = contenido;
         
         reader.Close();
      }
  // Console.WriteLine(String.Join(" ",contenedor));
   Console.WriteLine(contenedor.Length);
      return contenedor;
  }
    //lee los nombres de los documentos
    public static string [] nombres()
    {
      string [] filas = ReadFolder();
      
      string [] direccion = new string [filas.Length];

      string [] name = new string [filas.Length];

      for (int i = 0; i < filas.Length; i++)
      {
        direccion[i] = filas[i];
        name[i] = Path.GetFileName(direccion[i]);
      }
     return name ;
    }
    //eliminar caracteres raros ,signos de puntuacion y mayusculas

    public static string [] quitar ()
    {
      string [] texto = ReadFiles();
      string [] remover = new string [] {"@",",",".",";","&","$","#","%","+","=","?","`","!","~","^","*"};
      char a = 'á';
      char e = 'é';
      char ini = 'í';
      char o = 'ó';
      char u = 'ú';
      for (int i = 0; i < remover.Length; i++)
      {

        for (int j = 0; j < texto.Length; j++)
        {
          for (int k = 0; k < texto[j].Length; k++)
          {
          texto[j] = texto[j].Replace(remover[i],string.Empty);
          texto[j] = new string(texto[j].Where(c => !char.IsPunctuation(c)).ToArray());
          texto[j] = texto[j].ToLower();
          texto[j] = texto[j].Replace(a,'a');
          texto[j] = texto[j].Replace(e ,'e');
          texto[j] = texto[j].Replace(ini,'i');
          texto[j] = texto[j].Replace(o,'o');
          texto[j] = texto[j].Replace(u,'u');
          }
      }
      }
      return texto;
    }
    public static string quitado(string query)
    {
      string texto = query.ToLower();
      string [] remover = new string [] {"@",",",".",";","&","$","#","!","%","+","=","^","?","`","~","!","*"};
      char a = 'á';
      char e = 'é';
      char ini = 'í';
      char o = 'ó';
      char u = 'ú';
      for (int i = 0; i < remover.Length; i++)
      {
          texto= texto.Replace(remover[i],String.Empty);
          texto = texto.Replace(a,'a');
          texto = texto.Replace(e ,'e');
          texto = texto.Replace(ini,'i');
          texto = texto.Replace(o,'o');
          texto = texto.Replace(u,'u');
      }
       // Console.WriteLine(texto);
         //Console.WriteLine(texto.Length);
      
      return texto;
    }        
    
    
      public static int levenshteinDistance(string palabra1 , string palabra2, out double porcentaje )
     {
      porcentaje = 0;

      //d es una tabla con m+1 renglones y n+1 columnas
      int costo = 0;
        int m = palabra1.Length;
        int n = palabra2.Length;
      int [,] d =new int[m+1,n+1];

      //verificar que exista algo para comparar
      if(n==0) return m;
      if(m==0) return n;

       //llena la primera columna y la primera fila
        for (int i = 0; i <= m; d[i,0] = i++);
       for (int j = 0; j <= n; d[0,j] = j++);
        //recorrer la matriz llenando cada uno de los pesos
      //i columnas , j renglones
      for (int i = 1; i <= m; i++)
      {
        //recorre para j
        for (int j = 1; j <= n; j++)
        {
            costo = (palabra1[i - 1] == palabra2[j - 1]) ? 0:1 ;
     
     
            d[i,j] = Math.Min(Math.Min(d[i - 1,j] + 1 , d[i,j - 1] +1),d[i -1,j -1]+ costo);
        }
        
      }
      //calcular el porcentaje de cambios de la palabra
      if(palabra1.Length > palabra2.Length)
       {
             porcentaje = ((double)d[m,n]/(double)palabra1.Length);
       }
      else
       {
        porcentaje = ((double)d[m,n]/(double)palabra2.Length);
      }
       return d[m,n];
     }
 }
}  

  
    
 
 