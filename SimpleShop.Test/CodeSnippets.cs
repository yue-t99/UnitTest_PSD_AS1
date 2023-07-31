using System;

namespace CodeSnippets{
    
    public class CodeSnippets{
        
        /// <summary>
        /// Describe the function here
        /// Rating 1
        /// </summary>
        /// <returns>bool</returns>
        public static bool function1(string pattern) {
            // This function is a static function
            // which means that it can be called even if the corresponding class is not declared
            // This function requires a input of string, and the out put is bool--True or False
            var parts = pattern.ToCharArray();
            // Firstly turns the input string to a array type, making it much easier to process the data
            Array.Reverse(parts);
            // using the Reverse function in Array, reversing the order of strings
            var starp = (new string(parts)).ToLower();
            
            var b = pattern.ToLower().Equals(starp);
            // test if the reversed string is the same as the initial string
            // if same return true, if not return false
            return b;
            // So actually this function is used to check if the string is symmetrical or not
        }


        /// <summary>
        /// Describe the function here.
        /// Rating 3
        /// </summary>
        /// <returns></returns>
        public static int function2(int[] numbers){
            // This function is a static function
            // which means that it can be called even if the corresponding class is not declared
            // This function requires a input of an array of int type, and the out put is a number of int
            for (var h = numbers.Length / 2; h > 0; h /= 2){
            // The array is grouped by certain increments of the subscript,
            // and each group is sorted using the direct insertion sorting algorithm;
            // as the increment decreases, each group contains more and more keywords,
            // when the increment decreases to 1, the entire file is divided into exactly one group, the algorithm terminates
                for (var i = h; i < numbers.Length; i += 1){
                    var temp = numbers[i];
                    int t;
                    for (t = i; t >= h && numbers[t - h] > temp; t -= h){
                        numbers[t] = numbers[t - h];
                    }
                    numbers[t] = temp;
                }
            }
            return 0;
            // Actually the return number do not count but the input array will be changed following a order
            // which is from smallest to the largest
            // This is a classical algorithm called Schell-Metznet
        }
    }
}