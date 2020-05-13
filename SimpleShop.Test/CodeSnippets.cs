using System;

namespace CodeSnippets{
    
    public class CodeSnippets{
        
        /// <summary>
        /// Describe the function here
        /// Rating 1
        /// </summary>
        /// <returns>bool</returns>
        static bool function1(string pattern) {
            var parts = pattern.ToCharArray();
            Array.Reverse(parts);
            var starp = (new string(parts)).ToLower();
            
            var b = pattern.ToLower().Equals(starp);
            return b;
        }
        
        
        /// <summary>
        /// Describe the function here.
        /// Rating 3
        /// </summary>
        /// <returns></returns>
        public static int function2(int[] numbers){
            for (var h = numbers.Length / 2; h > 0; h /= 2){
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
        }
    }
}