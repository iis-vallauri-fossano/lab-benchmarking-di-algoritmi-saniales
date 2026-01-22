namespace lesson
{
    public class Program
    {
        /// <summary>
        /// The main entrypoint of your application.
        /// </summary>
        /// <param name="args">The arguments passed to the program</param>
        public static void Main(string[] args)
        {
            // definire:
            // - l'algoritmo (o gli algoritmi) di cui fare il benchmark
            //   in questo caso "Algoritmi di ricerca"
            // - i test cases (casi numerosi ma univoci, uguali per tutti)
            //   su cui far girare gli algoritmi. I test cases spesso hanno un nome
            //   "ordinato", "ordinato al contrario" "casuale" etc...
            // - calcolare i tempi (media, varianza)

            int choice = Menu();

            switch (choice)
            {
                case 1:
                    BenchmarkSearch();
                    break;
                case 2:
                    BenchmarkSort();
                    break;
                default:
                    Console.WriteLine("Scelta non valida");
                    break;
            }
        }

        private static void BenchmarkSort()
        {
            throw new NotImplementedException();
        }

        private static void BenchmarkSearch()
        {
            Console.WriteLine("Benchmark degli algoritmi di ricerca...");

            // Abbiamo (algoritmi)
            // - ricerca sequenziale
            // - ricerca sequenziale ottimizzata
            // - ricerca binaria
            //
            // Abbiamo (casi)
            // - vettori di dimensioni diverse
            // - vettori ordinati e non
            // - vettori ordinati al contrario
            // - vettori parzialmente ordinati
            //
            // Abbiamo (unità di misura target) = ms

            int[][] cases = GenerateBenchmarkCases(10, false);
            int[][] sortedCases = GenerateBenchmarkCases(10, true);

            double[] sequentialSearchTimes = BenchmarkSequentialSearch(cases);
            double[] sortedSequentialSearchTimes = BenchmarkSequentialSearch(sortedCases);
            double[] optimizedSequentialSearchTimes = BenchmarkOptimizedSequentialSearch(sortedCases);
            double[] binarySearchTimes = BenchmarkBinarySearch(sortedCases);

            PrintTimes("Ricerca sequenziale", sequentialSearchTimes);
            PrintTimes("Ricerca sequenziale (array ordinato)", sortedSequentialSearchTimes);
            PrintTimes("Ricerca sequenziale ottimizzata (array ordinato)", optimizedSequentialSearchTimes);
            PrintTimes("Ricerca binaria (array ordinato)", binarySearchTimes);
        }

        /// <summary>
        /// Stampa i tempi di un benchmark.
        /// </summary>
        /// <param name="name">Nome visualizzato del benchmark</param>
        /// <param name="times">I tempi da stampare</param>
        private static void PrintTimes(string name, double[] times)
        {
            Console.WriteLine($"{name} :");

            // media
            double average = times[0];
            for (int i = 1; i < times.Length; i++)
            {
                average += times[i];
            }
            average /= times.Length;

            // scarto quadratico medio
            double discardAvg = 0;
            for (int i = 0; i < times.Length; i++)
            {
                double discard = times[i] - average;
                discardAvg += discard * discard;
            }
            discardAvg = Math.Sqrt(discardAvg);

            Console.WriteLine($"Media (ms): {average}, Scarto quadratico medio (ms): {discardAvg}");
        }

        private static double[] BenchmarkBinarySearch(int[][] cases)
        {
            double[] times = new double[cases.Length];

            for (int i = 0; i < cases.Length; i++)
            {
                times[i] = BenchmarkBinarySearchCase(cases[i]);
            }

            return times;
        }

        private static double BenchmarkBinarySearchCase(int[] benchCase)
        {
            // AI SOLI FINI DEL BENCHMARK PRECISO: NON DA STUDIARE
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // cerchiamo sempre l'elemento al fondo, per comodità, ma si può cambiare
            int index = BinarySearch(benchCase, benchCase[benchCase.Length - 1]);
            if (index == -1)
            {
                Console.WriteLine("ERRORE IMPORTANTE");
            }

            watch.Stop();

            return watch.Elapsed.TotalMilliseconds;
        }

        private static int BinarySearch(int[] benchCase, int value)
        {
            int inf = 0;
            int sup = benchCase.Length - 1;
            
            while(inf < sup)
            {
                int mid = (inf + sup) / 2;

                if (value > mid)
                {
                    inf = mid + 1;
                    
                }
                else if (value < mid)
                {
                    sup = mid - 1;
                    
                }
                else
                {
                    return mid;
                }
            }

            return -1;
        }

        private static double[] BenchmarkOptimizedSequentialSearch(int[][] cases)
        {
            double[] times = new double[cases.Length];

            for (int i = 0; i < cases.Length; i++)
            {
                times[i] = BenchmarkOptimalSequentialSearchCase(cases[i]);
            }

            return times;
        }

        private static double BenchmarkOptimalSequentialSearchCase(int[] benchCase)
        {
            // AI SOLI FINI DEL BENCHMARK PRECISO: NON DA STUDIARE
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // cerchiamo sempre l'elemento al fondo, per comodità, ma si può cambiare
            int index = OptimalSequentialSearch(benchCase, benchCase[benchCase.Length - 1]);
            if (index == -1)
            {
                Console.WriteLine("ERRORE IMPORTANTE");
            }

            watch.Stop();

            return watch.Elapsed.TotalMilliseconds;
        }

        private static int OptimalSequentialSearch(int[] benchCase, int value)
        {
            for (int i = 0; i < benchCase.Length; i++)
            {
                if (benchCase[i] == value)
                {
                    return i;
                }
                else if (benchCase[i] > value)
                {
                    return -1;
                }
            }

            return -1;
        }

        /// <summary>
        /// Effetua un benchmark della ricerca sequenziale
        /// </summary>
        /// <param name="cases">I bench cases da usare</param>
        /// <returns>I tempi dei benchmark</returns>
        private static double[] BenchmarkSequentialSearch(int[][] cases)
        {
            double[] times = new double[cases.Length];
            
            for (int i = 0; i < cases.Length; i++)
            {
                times[i] = BenchmarkSequentialSearchCase(cases[i]);
            }

            return times;
        }

        /// <summary>
        /// Restituisce il tempo di benchmark per un singolo caso di 
        /// ricerca sequenziale.
        /// </summary>
        /// <param name="benchCase">il bench case</param>
        /// <returns>Il tempo del benchmark</returns>
        private static double BenchmarkSequentialSearchCase(int[] benchCase)
        {
            // AI SOLI FINI DEL BENCHMARK PRECISO: NON DA STUDIARE
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // cerchiamo sempre l'elemento al fondo, per comodità, ma si può cambiare
            int index = SequentialSearch(benchCase, benchCase[benchCase.Length - 1]);
            if (index == -1)
            {
                Console.WriteLine("ERRORE IMPORTANTE");
            }

            watch.Stop();

            return watch.Elapsed.TotalMilliseconds;
        }

        private static int SequentialSearch(int[] benchCase, int value)
        {
            for (int i = 0; i < benchCase.Length; i++)
            {
                if (benchCase[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Genera i vettori da usare in tutti i benchmark:
        /// - vettori di dimensioni diverse
        /// - vettori ordinati e non
        /// - vettori ordinati al contrario
        /// - vettori parzialmente ordinati
        /// </summary>
        /// <param name="n">Il numero di elementi da generare</param>
        /// <param name="sorted">Se vero, </param>
        /// <returns>I vettori generati</returns>
        private static int[][] GenerateBenchmarkCases(int n, bool sorted)
        {
            int[][] cases = new int[n][];

            for(int i = 0; i < n; i++)
            {
                cases[i] = GenerateRandomBenchmarkCase(100000, sorted);
            }

            return cases;
        }

        /// <summary>
        /// Genera un singolo case (un vettore casuale)
        /// </summary>
        /// <returns>Un vettore generato casualmente</returns>
        private static int[] GenerateRandomBenchmarkCase(int n, bool sorted)
        {
            Random rnd = new Random();
            int[] benchCase = new int[n];

            for (int i = 0; i < n; i++)
            {
                // se voglio una generazione ordinata, devo
                // poter manipolare il minimo (deve essere il valore
                // dell'elemento precedente + 1)
                int min;
                if(!sorted || i == 0)
                {
                    // tuttavia al primo giro non ho un precedente,
                    // quindi devo impostare zero.
                    //
                    // Cosa che devo fare anche qualora la generazione
                    // ordinata io non la voglia proprio.
                    min = 0;
                }
                else
                {
                    min = benchCase[i - 1] + 1;
                }

                int max = min + 10000;

                // A quel punto la generazione procede come prima
                benchCase[i] = rnd.Next(min, max);
            }

            return benchCase;
        }

        private static int Menu()
        {
            int choice;

            do
            {
                Console.WriteLine("1 -> Benchmark ricerca");
                Console.WriteLine("2 -> Benchmark ordinamento");

                choice = Convert.ToInt32(Console.ReadLine());
            }
            while (choice < 1 || choice > 2);

            return choice;
        }
    }
}
