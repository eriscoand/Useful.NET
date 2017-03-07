using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MSExchangeKeys
{
    public string mse_change_key { get; set; }
    public string mse_key { get; set; }

    public MSExchangeKeys(string mse_change_key, string mse_key)
    {
        this.mse_change_key = mse_change_key;
        this.mse_key = mse_key;
    }
}