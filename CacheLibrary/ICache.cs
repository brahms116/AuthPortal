using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheLibrary;

public interface ICache<T>
{
    Task<T?> GetValue(string key);
    Task SetValue(string key, T value);
    Task<T?> ClearValue(string key);
}
