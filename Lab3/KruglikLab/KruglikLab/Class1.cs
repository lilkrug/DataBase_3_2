using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.UserDefined, Name = "Route", MaxByteSize = 16)]
public struct Route : INullable, IBinarySerialize
{
    private string _destination;
    private int _length;
    public SqlString Destination
    {
        get { return new SqlString(_destination); }
        set
        {
            if (value == null)
            {
                _destination = string.Empty; return;
            }
            else
            {
                _destination = value.ToString();
            }
        }
    }
    public SqlInt32 Length
    {
        get
        {
            return new SqlInt32(_length);
        }
        set
        {
            _length = (int)value;
        }
    }
    public override string ToString()
    {
        return $"Destination: {_destination},Length:{_length}";
    }

    public bool IsNull
    {
        get
        {
            return string.IsNullOrEmpty(_destination);
        }
    }

    public static Route Null
    {
        get
        {
            Route h = new Route();
            h._destination = string.Empty;
            return h;
        }
    }

    public static Route Parse(SqlString s)
    {
        if (s.IsNull)
            return Route.Null;
        Route u = new Route();
        string[] xyz = s.Value.Split(",".ToCharArray());
        u.Destination = xyz[0];
        u.Length = SqlInt32.Parse(xyz[1]);

        return u;
    }

    public void Read(BinaryReader r)
    {
        _destination = r.ReadString();
        _length = r.ReadInt32();
    }

    public void Write(BinaryWriter w)
    {
        w.Write(_destination);
        w.Write(_length);
    }
}