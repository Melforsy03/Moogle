
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using System.Threading.Tasks.Sources;
using System.Net.Security;
using System.Configuration.Assemblies;
using System.Net.WebSockets;
using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Tracing;
using System.Text;
using System.Collections.Generic;
using Name;
using TACOS;
using operadores;
namespace TAMAL
{
   
   class Escore
 {
  
      
    
    //obtiene el vocabulario general del corpus
 public static List<string> vocabulary(string [] docs)
{ 
  List<string> vocab = new List<string>();

   for (int i = 0; i < docs.Length; i++)
   {
       List <string> removidas = new List<string>{docs[i]};

      removidas = docs[i].Split(' ').ToList();

    for (int j = 0; j < removidas.Count; j++)
    {
      if (!vocab.Contains(removidas[j]))
      {
         vocab.Add(removidas[j]);
      }
    }
   }
 
    return vocab;
}

//obtiene el tfidf del vector del query
 public static  Dictionary <string,Dictionary<string , double >> vectors(string query ,string [] docs, string [] name ,string queriado)
 {
    List <string> cadena = new List<string>(){query};
    cadena = query.Split(' ').ToList();
     Dictionary <string , double > TFIDF= new Dictionary<string , double >();
    Dictionary <string ,Dictionary<string, double >> vector = new Dictionary<string ,Dictionary<string , double >>();

      for (int k = 0; k < docs.Length; k++)
      {
        TFIDF = dicc(query,docs[k],docs,queriado);

        //Console.WriteLine(TFIDF);
         vector.Add(name [k],TFIDF);
      
      }
       return vector;
 }
 //introduce los tfidf del query por documentos
 static Dictionary<string, double > dicc (string query, string docs ,string [] doc,string queriado)
 {
  
    Dictionary <string , double > TFIDF= new Dictionary<string , double >();
    List<string> palabra = new List<string>{query};
    palabra = query.Split(' ').ToList();
  
    for (int i = 0; i < palabra.Count; i++)
    {
     // Console.WriteLine(palabra[i]);
      double tf = TF(palabra[i],docs,doc);
      if(palabra[i] == String.Empty) tf = 0;
     // Console.WriteLine(tf);
        double idf = IDF(palabra[i] , doc , queriado);
     
      double tfidf = tf*idf;
      if(tf == 0 && idf == 0) tfidf = 0;
     //Console.WriteLine(tfidf);
      TFIDF.Add(palabra[i],tfidf);
    }
 
    return TFIDF;

 }
 //obtener tf
 public static  double  TF(string palabra ,string documento ,string [] docs)
 {
   float con = 0;

   List<string> lista = new List<String>{documento};

   lista = documento.Split(' ').ToList();

   for (int i = 0; i < lista.Count; i++)
   {
    if(lista[i] == palabra)
    con++;
   }
   
    double  TF = con/docs.Length ;
     
  return TF;

 }
 //obtener IDF
 public static double IDF(string palabra , string [] documento, string queriado)
 {
    List<string> aste = operando.asterisco(queriado);
    int [] numeros = operando.prioridad(aste);
     double contar = 0;
    for (int i = 0; i < documento.Length; i++)
    {
        if(documento[i].Contains(palabra))

        contar++;
    }
    double CantDocs = documento.Length;

    double IDF = Math.Log(CantDocs/contar + 1);
   
   if(palabra == String.Empty)IDF = 0;
    return IDF;
 }
 //valores de las palabras en los docuemntos
 public static Dictionary<string ,Dictionary<string , double >> TFIDF (List <string> vocab , string [] docs , string [] name,string queriado)
 {
   Dictionary<string ,Dictionary<string, double >> valores = new Dictionary<string ,Dictionary<string, double >>();
   Dictionary<string , double > valor = new Dictionary<string, double >();
   int k = 0;
   
   for (int i = 0; i < docs.Length; i++)
   {
         
      for (int j = 0; j < vocab.Count; j++)
      {
         if (docs[i].Contains(vocab[j]))
         {
            try
         {
            double tf = TF(vocab[j] , docs [i] ,docs);

             double idf = IDF(vocab[j],docs,queriado);
         
           double  tfidf = tf*idf;
            if(vocab[j] != " " )
            valor.Add(vocab[j],tfidf);
         }
             catch (System.Exception)
         {
         }
         }
         }
      
         valores.Add(name[k],valor);
      k++;
    }
     return valores;
 }
    //calculo de similitud entre dos vectores
   public  static  double  CalSimilitudCos( double  [] query ,  double  [] documento)
  {
         double  producto = PRODUCTO(query,documento);
        // Console.WriteLine(producto);
         double  magnitudA = MAGNITUD(documento);
        // Console.WriteLine(magnitudA);
         double  magnitudB = MAGNITUD(query);
        // Console.WriteLine(magnitudB);
         double  prod =  producto /magnitudA*magnitudB  ;
        // if(producto == 0 && magnitudA*magnitudB == 0) prod = 0;
       // Console.WriteLine(producto / magnitudA*magnitudB);
        return prod;

  }
    private static  double  PRODUCTO( double  [] query, double  [] documento )
    {
         double  producto = 0;
        for (int i = 0; i < query.Length; i++)
        {
          if(query[i] == 0 && documento[i] == 0)documento[i] = 1;
           producto += query[i]*documento[i];
          
        }
        
        return producto;
    }
    private static double MAGNITUD( double  [] vector)
    {
        return Math.Sqrt(PRODUCTO(vector,vector));
    }
     private static int levenshteinDistance(string palabra1 , string palabra2, out  double  porcentaje )
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
             porcentaje = (( double )d[m,n]/( double )palabra1.Length);
       }
      else
       {
        porcentaje = (( double )d[m,n]/( double )palabra2.Length);
      }
       return d[m,n];
    }
}
}
