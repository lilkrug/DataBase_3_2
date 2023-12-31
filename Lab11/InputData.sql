
INSERT INTO [Customer] (CustomerName) VALUES
('Лукоил'),
('Белнефтехим'),
('АйТекАрт'),
('СильверТур'),
('ТурБай'),
('Бабушкина Крынка');

INSERT INTO [Order] (CustomerName, ServiceId, OrderDate, OrderExec) VALUES 
('Бабушкина Крынка',6,'19.05.2023','28.07.2023'),
('АйТекАрт',1,'11.06.2023','12.06.2023'),
('Лукоил',1,'11.01.2023','12.02.2023'),
('Белнефтехим',1,'12.02.2023','12.03.2023');

INSERT into City (CityName) values
('Минск'),('Могилев'),('Брест'),('Витебск'),('Гродно'),
('Гомель'),('Москва'),('Вильнюс'),('Варшава'),
('Белосток'),('Киев');

INSERT into ServiceType (ServiceName, RouteName) values 
('Перевозка людей'),
('Перевозка автомобилей'),
('Перевозка древесины'),
('Перевозка жидкостей пищевой промышленности'),
('Перевозка жидкостей химической промышленности');

INSERT into Route (RouteName, Distance, DeparturePoint, ArrivalPoint) values
('Минск-Белыничи-Могилев','212','Минск','Могилев'),
('Минск-Орша-Витебск','294','Минск','Витебск'),
('Витебск-Минск','246','Витебск','Минск'),
('Минск-Гомель','213','Минск','Гомель'),
('Гомель-Минск','213','Гомель','Минск'),
('Минск-Гродно','246','Минск','Гродно'),
('Гродно-Минск','246','Гродно','Минск'),
('Минск-Брест','234','Минск','Брест'),
('Брест-Минск','234','Брест','Минск'),
('Минск-Москва','586','Минск','Москва'),
('Минск-Варшава','601','Минск','Варшава'),
('Минск-Белосток','496','Минск','Белосток'),
('Минск-Киев','563','Минск','Киев');

INSERT into Service (ServiceType, RouteName) values
('Перевозка людей','Минск-Белыничи-Могилев'),
('Перевозка людей','Минск-Орша-Витебск'),
('Перевозка людей','Минск-Варшава'),
('Перевозка людей','Минск-Белосток'),
('Перевозка людей','Минск-Киев'),
('Перевозка людей','Витебск-Минск'),

('Перевозка жидкостей химической промышленности','Минск-Гомель'),
('Перевозка жидкостей пищевой промышленности','Минск-Гомель'),
('Перевозка древесины','Минск-Гомель'),
('Перевозка автомобилей','Минск-Гомель'),

('Перевозка жидкостей химической промышленности','Брест-Минск'),
('Перевозка жидкостей пищевой промышленности','Брест-Минск'),
('Перевозка древесины','Брест-Минск'),
('Перевозка автомобилей','Брест-Минск'),

('Перевозка жидкостей химической промышленности','Минск-Гомель'),
('Перевозка жидкостей пищевой промышленности','Минск-Гомель'),
('Перевозка древесины','Минск-Гомель'),
('Перевозка автомобилей','Минск-Гомель');




