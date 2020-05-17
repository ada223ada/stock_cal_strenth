using System.Collections.Generic;
using System.Security.Policy;

namespace CalStrength.Configuration
{

    public class RedisConfigs
    {
        public RedisConfig  redis{ get; set; }
    }
    public class RedisConfig
    {
        // redis数据库索引(默认为0)，我们使用索引为3的数据库，避免和其他数据库冲突
        public int database { get; set; }
        // redis服务器地址（默认为loaclhost）
        public string host { get; set; }
        // redis端口（默认为6379）
        public short post { get; set; }
        // redis访问密码（默认为空）
        public string password { get; set; }
        // redis连接超时时间（单位毫秒）
        public int timeout { get; set; }
        // redis连接池配置
        public RedisPool pool { get; set; }
    }
    public class RedisPool
    {
        // 最大可用连接数（默认为8，负数表示无限）
        public int maxActive{ get; set; }
        // 最大空闲连接数（默认为8，负数表示无限）
        public int maxIdle{ get; set; }
        // 最小空闲连接数（默认为0，该值只有为正数才有用）
        public int minIdle{ get; set; }
        // 从连接池中获取连接最大等待时间（默认为-1，单位为毫秒，负数表示无限） 
	    public int maxWait{ get; set; }
    }
    //public class RedisConfigs
    //{
    //    public List<RedisConfig> redisConfigs { get; set; }
    //    public Site site { get; set; }
    //}
    //public class RedisConfig
    //{
    //    public List<ConfigHostAndPort> hostAndPorts { get; set; }
    //    public string tableName { get; set; }
    //    public int maxTotal { get; set; }
    //    public int maxIdle { get; set; }
    //    public long maxWaitMillis { get; set; }
    //    public long time { get; set; }
    //    public int pageCnt { get; set; }

    //}
    //public class ConfigHostAndPort
    //{
    //    public string IP { get; set; }
    //    public int port { get; set; }
    //} 
}
