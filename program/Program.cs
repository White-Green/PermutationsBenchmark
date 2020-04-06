using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace program
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            foreach (var p in Permutations(8))
            {
                foreach (var i in p)
                {
                    Console.Write(i);
                }

                Console.WriteLine();
            }

            sw.Stop();
            Console.Error.WriteLine(sw.Elapsed.TotalMilliseconds);
        }
#if LIST
        private static List<int[]> Permutations(int n)
        {
            var list = new List<int[]>();
            var array = Enumerable.Range(0, n).ToArray();
            while (true)
            {
                var res = new int[n];
                array.CopyTo(res, 0);
                list.Add(res);
                int i;
                for (i = n - 2; i >= 0; i--)
                    if (array[i] < array[i + 1])
                        break;
                if (i < 0) break;
                int j;
                for (j = n - 1; j >= 0; j--)
                    if (array[i] < array[j])
                        break;
                int tmp = array[i];
                array[i] = array[j];
                array[j] = tmp;
                Array.Reverse(array, i + 1, n - i - 1);
            }

            return list;
        }
#elif LIST_INIT
        private static List<int[]> Permutations(int n)
        {
            int fact = 1;
            for (int i = 2; i <= n; i++) fact *= i;
            var list = new List<int[]>(fact);
            var array = Enumerable.Range(0, n).ToArray();
            while (true)
            {
                var res = new int[n];
                array.CopyTo(res, 0);
                list.Add(res);
                int i;
                for (i = n - 2; i >= 0; i--)
                    if (array[i] < array[i + 1])
                        break;
                if (i < 0) break;
                int j;
                for (j = n - 1; j >= 0; j--)
                    if (array[i] < array[j])
                        break;
                int tmp = array[i];
                array[i] = array[j];
                array[j] = tmp;
                Array.Reverse(array, i + 1, n - i - 1);
            }

            return list;
        }
#elif YIELD
        private static IEnumerable<int[]> Permutations(int n)
        {
            var array = Enumerable.Range(0, n).ToArray();
            while (true)
            {
                yield return array;
                int i;
                for (i = n - 2; i >= 0; i--)
                    if (array[i] < array[i + 1])
                        break;
                if (i < 0) break;
                int j;
                for (j = n - 1; j >= 0; j--)
                    if (array[i] < array[j])
                        break;
                int tmp = array[i];
                array[i] = array[j];
                array[j] = tmp;
                Array.Reverse(array, i + 1, n - i - 1);
            }
        }
#elif ENUM
        private static IEnumerable<int[]> Permutations(int n)
        {
            return new PermutationEnumerable(n);
        }

#else
        private static List<int[]> Permutations(int n)
        {
            throw new NotImplementedException();
        }
#endif
    }

    class PermutationEnumerable : IEnumerable<int[]>
    {
        private readonly int _n;

        public PermutationEnumerable(int n)
        {
            _n = n;
        }

        public IEnumerator<int[]> GetEnumerator()
        {
            return new PermutationEnumerator(_n);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class PermutationEnumerator : IEnumerator<int[]>
    {
        private readonly int _n;

        public PermutationEnumerator(int n)
        {
            _n = n;
            Reset();
        }

        public bool MoveNext()
        {
            bool ret = res;

            var atmp = Current;
            Current = Next;
            Next = atmp;

            int i;
            for (i = _n - 2; i >= 0; i--)
                if (Next[i] < Next[i + 1])
                    break;
            if (i < 0)
            {
                res = false;
                return ret;
            }

            int j;
            for (j = _n - 1; j >= 0; j--)
                if (Next[i] < Next[j])
                    break;
            int tmp = Next[i];
            Next[i] = Next[j];
            Next[j] = tmp;
            Array.Reverse(Next, i + 1, _n - i - 1);

            return ret;
        }

        public void Reset()
        {
            Current = Enumerable.Range(0, _n).ToArray();
            Next = Enumerable.Range(0, _n).ToArray();
            res = true;
        }

        public int[] Current { get; private set; }
        private int[] Next;
        private bool res;

        object? IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}