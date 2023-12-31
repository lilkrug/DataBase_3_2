--  FROM UNIVER DB

-- get all auditoriums and auditorium type name where there are only 2 auditoriums of such size.
SELECT A.AUDITORIUM, T.AUDITORIUM_TYPENAME
	FROM (
		SELECT AUDITORIUM, AUDITORIUM_TYPE, COUNT(AUDITORIUM_CAPACITY) OVER (PARTITION BY AUDITORIUM_CAPACITY) AS AUD_COUNT
			FROM AUDITORIUM) AS A
	JOIN AUDITORIUM_TYPE AS T
		ON A.AUDITORIUM_TYPE = T.AUDITORIUM_TYPE
	WHERE A.AUD_COUNT = 2;

-- return faculty names in order by how much subjects does faculty have, where subjects name contain '���'.

SELECT TOP(2) F.FACULTY_NAME
	FROM (SELECT TOP(23) COUNT(P.FACULTY) OVER (ORDER BY P.FACULTY RANGE CURRENT ROW) AS PULPIT_COUNT, S.SUBJECT_NAME, P.FACULTY
			FROM PULPIT AS P
			INNER JOIN SUBJECT AS S
				ON S.PULPIT = P.PULPIT
			ORDER BY PULPIT_COUNT) AS RES
	INNER JOIN FACULTY AS F
		ON F.FACULTY = RES.FACULTY
	WHERE RES.SUBJECT_NAME LIKE '%���%'
-- TOPs are not good, but:
-- 1) in nested request ORDER BY is not allowed without TOP or smth. like that;
-- 2) TOP(2) is my alternative for DISTINCT and GROUP BY F.FACULTY_NAME, because both of them sort result in alphabeth order.

-- find students, that is before students, that have 'a ' in their name and everage mark in group is less than 6.0.
SELECT NAME
	FROM (SELECT *, LEAD(AVG_GROUP_NOTE) OVER (ORDER BY NAME) AS NEXT_NOTE
			FROM (SELECT G.IDGROUP, S.NAME, P.NOTE, AVG(P.NOTE + 0.0) OVER (PARTITION BY G.IDGROUP) AS AVG_GROUP_NOTE, LEAD(S.NAME) OVER (ORDER BY S.NAME) AS NEXT_NAME
					FROM GROUPS AS G
					JOIN STUDENT AS S
						ON S.IDGROUP = G.IDGROUP
					JOIN PROGRESS AS P
						ON P.IDSTUDENT = S.IDSTUDENT) AS U) AS LST
	WHERE NEXT_NAME LIKE '%� %' AND NEXT_NOTE < 6.0;


-- FROM EXAM DB

-- find salesreps who has less sales than average sales in office
SELECT EMPL_NUM, NAME, REP_OFFICE, SALES
	FROM (SELECT *, AVG(SALES) OVER (PARTITION BY REP_OFFICE) AS AVG_SALES_IN_OFFICE
			FROM SALESREPS) AS SLSRPS
	WHERE AVG_SALES_IN_OFFICE > SALES;

-- in monthes, when there were 4 or more orders, find 4th order (same monthes in different years are different monthes)
SELECT ORDER_NUM, ORDER_DATE, AMOUNT
	FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY YEAR(ORDER_DATE), MONTH(ORDER_DATE) ORDER BY ORDER_DATE) AS ROWNUM
			FROM ORDERS) AS BYYEAR
	WHERE ROWNUM = 4;

-- find penultimate orders in every year
SELECT ORDER_NUM
	FROM (SELECT *, COUNT(ORDER_NUM) OVER (PARTITION BY YEAR(ORDER_DATE) ORDER BY ORDER_NUM
				ROWS BETWEEN CURRENT ROW AND UNBOUNDED FOLLOWING) AS AFTERORDER
			FROM ORDERS) AS YR
	WHERE AFTERORDER = 2;--112992&113065

-- find last orders in every year
SELECT ORDER_NUM
	FROM (SELECT *, LAST_VALUE(ORDER_NUM) OVER (PARTITION BY YEAR(ORDER_DATE) ORDER BY ORDER_NUM
				ROWS BETWEEN CURRENT ROW AND UNBOUNDED FOLLOWING) AS AFTERORDER
			FROM ORDERS) AS YR
	WHERE ORDER_NUM = AFTERORDER;