using Microsoft.AspNetCore.Mvc;
using PerfectNumber.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PerfectNumber.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BolenHesaplama()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(int numberInput)
        {
            int sumOfDivisors = FindSum(numberInput);
            ViewBag.Result = $"{numberInput} sayısının bölenlerinin toplamı: {sumOfDivisors}";
            return View("BolenHesaplama");
        }

        private int FindSum(int sayi)
        {
            int toplam = 0;
            for (int i = 1; i <= sayi / 2; i++)
            {
                if (sayi % i == 0)
                {
                    toplam += i;
                }
            }
            return toplam;
        }

        public IActionResult MergeSort()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MergeSortHesaplama(string textboxInput)
        {
            if (string.IsNullOrWhiteSpace(textboxInput))
            {
                ViewBag.Result = "Lütfen geçerli bir giriş yapınız.";
                return View("MergeSort");
            }

            // Veriyi parse edip diziye dönüştürme
            string[] inputArray = textboxInput.Split(',', StringSplitOptions.RemoveEmptyEntries);

            try
            {
                int[] numbers = Array.ConvertAll(inputArray, int.Parse);
                int[] sortedArray = MergeSort(numbers);
                ViewBag.Result = $"Sıralanmış dizi: {string.Join(", ", sortedArray)}";
            }
            catch (FormatException)
            {
                ViewBag.Result = "Geçersiz giriş formatı. Lütfen sayıları virgülle ayırarak giriniz.";
            }

            return View("MergeSort");
        }


        private int[] MergeSort(int[] array)
        {
            if (array.Length <= 1)
                return array;

            int middle = array.Length / 2;
            int[] left = new int[middle];
            int[] right = new int[array.Length - middle];

            Array.Copy(array, 0, left, 0, middle);
            Array.Copy(array, middle, right, 0, array.Length - middle);

            left = MergeSort(left);
            right = MergeSort(right);

            return Merge(left, right);
        }

        private int[] Merge(int[] left, int[] right)
        {
            List<int> result = new List<int>();

            int leftIndex = 0;
            int rightIndex = 0;

            while (leftIndex < left.Length && rightIndex < right.Length)
            {
                if (left[leftIndex] <= right[rightIndex])
                {
                    result.Add(left[leftIndex]);
                    leftIndex++;
                }
                else
                {
                    result.Add(right[rightIndex]);
                    rightIndex++;
                }
            }

            while (leftIndex < left.Length)
            {
                result.Add(left[leftIndex]);
                leftIndex++;
            }

            while (rightIndex < right.Length)
            {
                result.Add(right[rightIndex]);
                rightIndex++;
            }

            return result.ToArray();
        }

        public IActionResult OzyinelemeliFonksiyon()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Faktoriyel(int numberInput)
        {
            int sumOfnumber = FaktoriyelHesapla(numberInput);
            ViewBag.Result = $"{numberInput} sayısının faktoriyeli : {sumOfnumber}";
            return View("OzyinelemeliFonksiyon");
        }

        private int FaktoriyelHesapla(int sayi)
        {
            if (sayi <= 1)
                return 1;
            else
                return sayi * FaktoriyelHesapla(sayi - 1);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
