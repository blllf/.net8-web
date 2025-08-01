

EF Core 框架

映射方式

- 按约定 : 基础
- 数据注解：特性
- Fluent API：编码方式 设计层面 关系映射因该是对立的

```c#
public class PlayerMap : IEntityTypeConfiguration<Player> 
它实现了接口 IEntityTypeConfiguration<Player>，这是 EF Core 提供的用于分离实体配置的机制。
这个接口属于 EF Core 的 Fluent API 配置方式。
作用是将实体类（如 Player）与其数据库映射逻辑分离，实现关注点分离（Separation of Concerns）。
实现该接口必须重写 Configure 方法，用来定义实体如何映射到数据库表结构。
    
Configure 方法
使用 EntityTypeBuilder<Player> 参数来配置 Player 类的映射规则。
常见配置包括：字段长度、是否可为空、主键、索引、外键、表名等
```

```
数据迁移命令
创建初始迁移：
当您首次设置您的模型并准备将其映射到数据库时，可以创建一个初始迁移。
dotnet ef migrations add InitialCreate	//未成功
dotnet ef database update

视频：
Add-Migration InitialCreate	//根据你当前正确的模型代码（包括静态的种子数据时间）生成一个全新的 InitialCreate 迁移文件。
Update-Database
Microsoft.EntityFrameworkCore.Tools 需要安装


问题：
1. 命令行乱码和**dotnet ef命令无法识别**。
方法1：在PowerShell中临时修复
chcp 65001
2. 解决 dotnet ef 命令无法识别的问题
dotnet tool install --global dotnet-ef
验证版本：dotnet ef --version

```

```
路由
约定路由 特性路由

DTO 数据传输 用于客户端 与 服务端 之间数据传输 
使用AutoMapper扩展
DTO 数据格式要求：
	1.	字段数量不同
	DTO可以只包含需要传输的部分字段
	实体中的敏感字段（如密码）可以不包含在DTO中
	2.	字段名称不同
	可以根据API需求重新命名字段
	例如：实体中是 FirstName，DTO中可以是 Name
	3.	字段类型不同
	可以进行类型转换
	例如：实体中是 DateTime，DTO中可以是 string 格式化的日期

```

```c#
1. 注册绑定可以通义放在单独的文件中
// 将IPlayerRepository接口与PlayerRepository实现类进行注册绑定。AddScoped表示注册为作用域生命周期的服务
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

2. 添加种子数据 => 大量的情况下也可以放在单独的文件下
  
```

```
Parameter
ZD全局级 和 Org机构参数
项目管理 SRM 聚合
from zd parameter
时间风暴

更改为OrgParameterService对应的方法：
Comteck.Market.Services.Promotion.Impl
PointGoodsSchemeService
897->
929->

namespace Comteck.Common.Services.Impl {
  public class TableInfoService

问题：
1. 查询POS参数，Task<List<parameter>> GetPosParameter
	参数是parameter，就写在ParameterService 中，然后出现orgParameterDao，把这个linq语句封装成方法调用对应Service？
2. Task<string> GetValue_NoCache_Async
	方法返回值既不是也不是，然后同时有，保留哪一个
3. 出现这种var list = await orgParameterDao.Value.TableUntracked
  .Where(x => x.Code != null && x.Code == CustomDbFunction.AnyInArray(codes.ToArray())
  && x.Org_Code == org_code).ToListAsync();
	要把这个封装成一个方法吗？
```





```c#
使用 Lazy<T> 在依赖注入中很常见，尤其是在复杂系统或存在 循环依赖 / 性能优化 / 延迟初始化 的场景下。
// 构造 OrgParameterService 时，并不立即创建 ParameterService
// 只有真正用到时（.Value），才创建

避免循环依赖（Circular Dependency）
假设：
OrgParameterService 依赖 IParameterService
ParameterService 又依赖 IOrgParameterService
这就形成了 A 依赖 B，B 又依赖 A 的死循环。如果都用 Lazy<T>，就可以打破这个循环：
// OrgParameterService 中
var param = _parameterService.Value; // ← 等真正调用时才解析，此时 ParameterService 可能已部分初始化
// ParameterService 中
var orgParam = _orgParameterService.Value;
👉 DI 容器可以先创建两个“壳”对象，再用 Lazy<T> 延迟获取对方，避免构造时直接报错。
    
    
await orgParameterDao.Value.TableUntracked.Where(x => x.Org_Code != null && x.Org_Code == CustomDbFunction.AnyInArray(orgCodes.ToArray())).ToListAsync();

TableUntracked 是一个自定义属性，通常在仓储模式中使用：
•	它返回一个 IQueryable<T> 查询对象
•	Untracked 表示查询时不跟踪实体状态，提高查询性能
•	适用于只读场景，避免 Entity Framework 的变更跟踪开销

1从 orgParameterDao 仓储的表中查询数据
2.	过滤条件：
	x.Org_Code != null：组织代码不为空
	x.Org_Code == CustomDbFunction.AnyInArray(...)：组织代码在指定的 orgCodes 数组中
3.	执行方式：异步执行查询并转换为 List

```

