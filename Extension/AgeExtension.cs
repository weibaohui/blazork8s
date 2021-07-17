using System;

namespace Extension
{
    public static class AgeExtensions
    {
        /// <summary>
        /// 计算时间年龄

        /// </summary>
        /// <param name="time"></param>
        /// <returns>
        /// </returns>
        /// <example>
        /// <code>
        /// DateTime.Now.AddSeconds(-20).Age()//20s
        /// DateTime.Now.AddMinutes(-20).Age()//20m
        /// DateTime.Now.AddHours(-20).Age()//20h
        /// DateTime.Now.AddDays(-20).Age()//20d
        /// DateTime.Now.AddYears(-20).Age()//20y
        /// </code>

        /// </example>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string Age(this DateTime time)
        {
            var count = DateTime.Now.Subtract(time).TotalSeconds;
            var str = count switch
            {
                <0                                                   => "0s",
                <=60                                                 => $"{count}s",
                >60 when count / 60 > 0 & count / 60 <= 60           => $"{count / 60:0}m",
                >60 when count / 60 / 60 > 0 & count / 60 / 60 <= 24 => $"{count / 60 / 60:0}h",
                >60 when count / 60 / 60 / 24 > 0 & count / 60 / 60 / 24 <= 365 =>
                    $"{count / 60 / 60 / 24:0}d",
                 >60 when count / 60 / 60 / 24 / 365  > 0 => $"{count / 60 / 60 / 24  / 365:0}y",
                _                                         => throw new ArgumentOutOfRangeException()
            };
            return str;
        }
        
        
    }
}
