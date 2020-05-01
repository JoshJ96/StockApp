using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginTest.Controllers
{
    public class Class1
    {
        public class Solution
        {
            public int[] TwoSum(int[] nums, int target)
            {
                Dictionary<int, int> hash = new Dictionary<int, int>();
                for (int i = 0; i < nums.Length; i++)
                {
                    int item = nums[i];
                    int minus = target - item;

                    bool check = hash.ContainsKey(minus);
                    if (check && hash[minus] != i)
                    {
                        return new int[] { hash[minus], i };
                    }
                    if (!hash.ContainsKey(item)) hash.Add(item, i);
                }
                return new int[] { };
            }
        }
    }
}