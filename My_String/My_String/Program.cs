using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace My_String
{


    /// <summary>
    /// Represents text as a series of Unicode characters;
    /// </summary>
    public class String
    {
        /// <summary>
        /// This array stores elements of this String instance;
        /// </summary>
        private char[] data;

        /// <summary>
        /// Gets the number of elements in current String object;
        /// </summary>
        public int Length { get; private set; }


        /// <summary>
        /// Initializes a new instance of the String class to the value indicated by an array of Unicode characters;
        /// </summary>
        /// <param name="value"></param>
        public String(char[] value)
        {
            data = new char[value.Length];

            value.CopyTo(data,0);

            Length = data.Length;
        }


        /// <summary>
        /// Initializes a new instance of the String class with specified length;
        /// </summary>
        /// <param name="length"></param>
        public String(int length)
        {
            data = new char[length];
        }


        /// <summary>
        /// Initializes a new instance of the String class complies the taken value;
        /// </summary>
        /// <param name="value"></param>
        public String(String value)
        {
            data = new char[value.Length];

            value.data.CopyTo(data,0);

            Length = value.Length;
        }


        /// <summary>
        /// Initializes a new instance of the String class complies the taken stringValue;
        /// </summary>
        /// <param name="stringValue"></param>
        public String(string stringValue)
        {
            data = new char[stringValue.Length];

            stringValue.CopyTo(0,data,0,stringValue.Length);

            Length = stringValue.Length;
        }


        /// <summary>
        /// Reports the index of the first occurence of the specified Unicode character in this string;
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int IndexOf(char c)
        {
            for (int i = 0; i < Length; ++i)
            {
                if(data[i] == c)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Reports the index of the first occurence of the specified string in this instance;
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
                
            if(value.Length > Length)
            {
                return -1;
            }


            for(int i = 0; i < Length; ++i)
            {
                if(i + value.Length > Length)
                {
                    return -1;
                }

                bool contains = true;
                for(int j = i; j < value.Length + i; ++j)
                {
                    if(this[j] != value[j-i])
                    {
                        contains = false;
                        break;
                    }
                }

                if(contains)
                {
                    return i;
                }
            }

            return -1;
        }


        /// <summary>
        /// Returns a value indicating whether the specified character occurs within this String;
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool Contains(char c)
        {
            if(IndexOf(c) == -1)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Returns a value indicating whether the specified String object occurs within this String;
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(String value)
        {
            if (IndexOf(value) == -1)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Deletes a specified number of characters from this instance beginning at a specified position;
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public String Remove(int startIndex, int count)
        {
            char[] toAssign = new char[Length - count];
            int counter = 0;

            for(int i = 0; i < Length; ++i)
            {
                if((i < startIndex) || (i >= startIndex + count))
                {
                    toAssign[counter] = data[i];
                    counter++;
                }
            }

            String toReturn = new String(toAssign);

            return toReturn;
        }



        /// <summary>
        /// Inserts a specified instance of String ata a specified index position in this instance;
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Insert(int startIndex, String value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (startIndex < 0 || startIndex > Length)
            {
                throw new ArgumentOutOfRangeException("startIndex");
            }

            int length = Length + value.Length;


            char[] toAssign = new char[length];
            int counter = 0;

            for (int i = 0; i < length; ++i)
            {
                if(i < startIndex)
                {
                    toAssign[counter] = data[i];
                }
                else if( (i >= startIndex) && (i < value.Length + startIndex))
                {
                    toAssign[counter] = value[i - startIndex];
                }
                else
                {
                    toAssign[counter] = data[i - value.Length];
                }
                counter++;
            }

            String toReturn = new String(toAssign);

            return toReturn;
        }


        /// <summary>
        /// Implicitly convert string(System.String) value to String;
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator String(string value)
        {
            String toReturn = new String(value);

            return toReturn;
        }


        /// <summary>
        /// Implicitly convert String value to string(System.String);
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator string(String value)
        {
            string toReturn = new string(value.data);

            return toReturn;
        }

        /// <summary>
        /// Gets(or sets) the character at a specified position.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public char this[int num]
        {
            get
            {
                return data[num];
            }
            set
            {
                data[num] = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the String class as a result of concatenation of firstString and secondString;
        /// </summary>
        /// <param name="firstString"></param>
        /// <param name="secondString"></param>
        /// <returns></returns>
        public static String operator +(String firstString, String secondString)
        {
            String toReturn = new String(firstString.Length + secondString.Length);

            firstString.data.CopyTo(toReturn.data,0);
            secondString.data.CopyTo(toReturn.data,firstString.Length);

            toReturn.Length = firstString.Length + secondString.Length;

            return toReturn;
        }

        public override bool Equals(object obj)
        {
            if((obj is String) == false)
            {
                return false;
            }

            String value = (String) obj;

            if(Length != value.Length)
            {
                return false;
            }

            for(int i = 0; i < Length; ++i)
            {
                if(data[i] != value[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check if two String values are equal;
        /// </summary>
        /// <param name="firstString"></param>
        /// <param name="secondString"></param>
        /// <returns></returns>
        public static bool operator ==(String firstString, String secondString)
        {
            bool toReturn = firstString.Equals(secondString);
            return toReturn;
        }

        /// <summary>
        /// Check if two String values are not equal;
        /// </summary>
        /// <param name="firstString"></param>
        /// <param name="secondString"></param>
        /// <returns></returns>
        public static bool operator !=(String firstString, String secondString)
        {
            bool toReturn = firstString.Equals(secondString);
            return toReturn;
        }
    }

    /// <summary>
   /// This class implements the Markov algorithm;
   /// </summary>
   public class MarkovAlgorithm
   {

       /// <summary>
       /// This class implements production in Markov algorithm;
       /// </summary>
       public class Production
       {
           /// <summary>
           /// The antecedent value;
           /// </summary>
           public String Antecedent { get; private set; }

           /// <summary>
           /// The consequent value;
           /// </summary>
           public String Consequent { get; private set; }

           public bool Final { get; private set; }

           /// <summary>
           /// This constructor creates instance with specified antecedent and consequent values;
           /// </summary>
           /// <param name="antecedent"></param>
           /// <param name="consequent"></param>
           /// <param name="final"></param>
           public Production(String antecedent, String consequent, bool final = false)
           {
               Antecedent = new String(antecedent);
               Consequent = new String(consequent);
               Final = final;
           }
       }


       /// <summary>
       /// The list of productions;
       /// </summary>
       public List<Production> Productions { get; private set; }



       /// <summary>
       /// This default constructor creates Markov algorithm with no productions;
       /// </summary>
       public MarkovAlgorithm()
       {
           Productions = new List<Production>();
       }


       /// <summary>
       /// Add new production to the production list;
       /// </summary>
       /// <param name="antecedent"></param>
       /// <param name="consequent"></param>
       /// <param name="final"></param>
       public void AddProduction(String antecedent, String consequent,bool final = false)
       {
           Production production = new Production(antecedent, consequent,final);

           Productions.Add(production);
       }


       /// <summary>
       /// This method returns the handled value;
       /// </summary>
       /// <param name="value"></param>
       /// <returns></returns>
       public String Execute(String value)
       {
           String toReturn = value;

           bool finalProduction;

           int counter = 0;

           do
           {
               finalProduction = true;
               for (int i = 0; i < Productions.Count; i++)
               {
                   if (toReturn.Contains(Productions[i].Antecedent))
                   {
                       int index = toReturn.IndexOf(Productions[i].Antecedent);

                       toReturn = toReturn.Remove(index,Productions[i].Antecedent.Length);
                       toReturn = toReturn.Insert(index,Productions[i].Consequent);

                       if (Productions[i].Final == false)
                       {
                           finalProduction = false;
                       }

                       break;
                   }
                   
               }

               counter++;

               //If algorithm executes more than 10000 productions it may be loop;
               if (counter >= 10000)
               {
                   throw new InvalidOperationException("Algorithm produces loop");
               }
           } while (finalProduction == false) ;

           return toReturn;
       }
   }

   [TestFixture]
   public class MarkovAlgorithmTest
   {
       [Test]
       public void AddProductionTest()
       {
           MarkovAlgorithm algorithm = new MarkovAlgorithm();
           algorithm.AddProduction("ab","b");

           Assert.AreEqual(algorithm.Productions.Count,1);
       }

       [Test]
       public void ExecuteTest()
       {
           MarkovAlgorithm algorithm = new MarkovAlgorithm();
           algorithm.AddProduction("ab","b");
           algorithm.AddProduction("ac","c");
           algorithm.AddProduction("aa","a");

           String returnedValue = algorithm.Execute("bacaabaa");

           String mustBe = "bcba";

           Assert.AreEqual(returnedValue,mustBe);
       }

       [Test]
       public void FinalProductionTest()
       {
           MarkovAlgorithm algorithm = new MarkovAlgorithm();
           algorithm.AddProduction("ab", "b",true);

           String returnedValue = algorithm.Execute("abcabd");

           String mustBe = "bcabd";

           Assert.AreEqual(returnedValue, mustBe);
       }

       [Test]
       [ExpectedException(typeof(InvalidOperationException))]
       public void LoopExceptionTest()
       {
           MarkovAlgorithm al = new MarkovAlgorithm();
           al.AddProduction("a", "aa");

           al.Execute("a");
       }

   }

    class Program
    {
        static void Main(string[] args)
        {
            MarkovAlgorithmTest test = new MarkovAlgorithmTest();

            test.AddProductionTest();
            test.ExecuteTest();
            test.FinalProductionTest();
            try
            {
                test.LoopExceptionTest();
            }
            catch (InvalidOperationException)
            {                
                
            }  

        }
    }
}
