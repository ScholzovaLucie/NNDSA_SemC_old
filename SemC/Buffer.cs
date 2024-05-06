using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemC
{
    public class Buffer<T> where T : struct // Omezení na hodnotové typy
    {
        private T[] array;
        private int capacity;
        private int count;

        // Konstruktor pro inicializaci kapacity bufferu
        public Buffer(int capacity)
        {
            this.capacity = capacity;
            array = new T[capacity];
            count = 0;
        }

        // Metoda pro přidání prvku do bufferu
        public void Add(T item)
        {
            if (count >= capacity)
            {
                throw new InvalidOperationException("Buffer je plný.");
            }
            array[count++] = item;
        }

        // Metoda pro zjištění počtu prvků v bufferu
        public int Count => count;

        // Metoda pro vyčištění bufferu
        public void Clear()
        {
            Array.Clear(array, 0, count);
            count = 0;
        }

        // Metoda pro block copy
       public void BlockCopy(byte[] source, int sourceIndex, int destinationIndex, int count)
    {
        if (sourceIndex < 0 || destinationIndex < 0 || count < 0)
            throw new ArgumentOutOfRangeException("Indexy a počet musí být nezáporné číslo.");

        if (sourceIndex + count > source.Length || destinationIndex + count > this.capacity)
            throw new ArgumentException("Operace kopírování přesahuje kapacitu zdroje nebo cíle.");

        Array.Copy(source, sourceIndex, this.array, destinationIndex, count);
    }


        // Metoda pro získání obsahu bufferu jako pole
        public T[] ToArray()
        {
            T[] resultArray = new T[count];
            Array.Copy(array, 0, resultArray, 0, count);
            return resultArray;
        }
    }
}
