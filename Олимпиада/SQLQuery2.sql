use OLYMP2022;

--1
go
SELECT emp.empno, emp.ename,
CASE 
    WHEN emp_payment.parent_id IS NULL THEN 'корень'
    WHEN emp_payment.is_leaf = 0 THEN 'внутренний узел'
    ELSE 'лист'
END AS node_type
FROM emp 
JOIN emp_payment ON emp.empno = emp_payment.empno;

--2
SELECT * FROM emp WHERE sal LIKE '%8%';
--3
go
SELECT empno, COUNT(*) AS cnt
FROM emp_payment
WHERE payment_date >= '01-01-2023' AND payment_date <= '31-12-2023'
GROUP BY empno
HAVING COUNT(*) >= 2
ORDER BY empno;

--4
go
SELECT emp.empno, emp.ename, MIN(payment_date) as gratitude_date
FROM emp
INNER JOIN emp_payment payment ON emp.empno = payment.empno
WHERE payment.amount > 0
GROUP BY emp.empno, emp.ename


--5
go
SELECT e1.empno, e2.empno, ABS(SUM(e1.amount) - SUM(e2.amount)) as difference
FROM emp_payment e1, emp_payment e2
WHERE e1.empno < e2.empno 
GROUP BY e1.empno, e2.empno 

--6
go
SELECT deptno, 
       COUNT(*) AS total,
       SUM(CASE WHEN sal > 1500 THEN 1 ELSE 0 END) AS high_sal_count,
       (SUM(CASE WHEN sal > 1500 THEN 1 ELSE 0 END) / COUNT(*)) * 100 AS high_sal_prcnt
FROM emp
WHERE sal > 1500
GROUP BY deptno
HAVING COUNT(*) >= 3
ORDER BY high_sal_prcnt DESC

--7
SELECT emp.ename, MAX(payment.amount) as max_bonus
FROM emp
INNER JOIN emp_payment payment ON emp.empno = payment.empno
WHERE payment.amount <= 300
GROUP BY emp.empno, emp.ename
HAVING COUNT(payment.amount) = 3;

--8
go
SELECT emp.ename, MAX(sub.total_amount) as max_bonus
FROM emp 
INNER JOIN (
  SELECT empno, SUM(amount) as total_amount
  FROM (
    SELECT empno, amount, ROW_NUMBER() OVER(PARTITION BY empno ORDER BY amount) as row_num
    FROM emp_payment
    WHERE amount <= 3
  ) as sub	
  WHERE sub.row_num = 3
  GROUP BY sub.empno
) as sub ON emp.empno = sub.empno
GROUP BY emp.empno, emp.ename

--9
go
SELECT denomination, COUNT(*)
FROM salary_banknotes
JOIN bank ON bank.id = salary_banknotes.bank_id
WHERE salary_id = <идентификатор зарплаты>
GROUP BY denomination
ORDER BY denomination DESC

