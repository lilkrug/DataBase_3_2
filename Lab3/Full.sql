--зачем нужно CLR - CLR (Common Language Runtime) Ц обще€зыкова€ среда исполнени€ Ц виртуальна€ машина, на которой исполн€ютс€  все приложени€,
--работающие в среде .NET. явл€етс€ реализацией концепции VES от компании Microsoft.
--—одержит в себе JIT-компил€тор (Just-In-Time).
--что такое сборка - „аще всего сборка Ч исполн€емый файл Ч двоичный файл
--что такое dll -ƒинамически подключаема€ библиотека позвол€юща€ многократное использование различными программными приложени€ми.
--зачем нужно на clr делать процедуры  --дл€ увелечени€ производительности

--CREATE ASSEMBLY FROM .. -- ещЄ один вариант добавлени€ assembly

Use Trucking
sp_configure 'clr enabled',1 --включаем поддержку CLR
go
alter database [Trucking] set trustworthy on

create type route
external name KruglikLab.Route -- создаем тип

drop type route

declare @mytask as route
set @mytask = 'okay	, 2'
print @mytask.ToString()									--выполн€ть одновременно три строки, Route Parse забирает эти значени€

CREATE PROCEDURE spSendEmail
@receiver nvarchar(30)
AS    
EXTERNAL NAME KruglikLab.[KruglikLab.StoredProcedures].SendEmailUsingCLR

drop procedure spsendemail

exec spSendEmail 'lilkrug2003@gmail.com' --вызов процедуры параметр это почта получател€
