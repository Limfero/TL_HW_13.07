use CarProduction

--insert
insert into PurchaseOrder
	(NameBuyer, DealershipId, CarId)
values
	('Дарья', 2, 1)

insert into Car
	(Model, BuildData, Price, ManufacturerId)
values
	('BMW S', '2021-09-22', 6780000, 2),
	('BMW S2', '2021-09-23', 6780000, 2),
	('Lada Niva Legend', '2021-05-22', 818900, 1)

--select
select * from Car
order by
	Price desc

select * from Car
where 
	Model like 'Lada%'

--update
update top(1) Car
	set 
		Model = 'Lada Granta Универсал'
	where 
		CarId = 1

--delete
delete top(1) from PurchaseOrder
where NameBuyer = 'Дарья'

--group by + having
select ManufacturerId CarCount from Car
group by ManufacturerId
having count(*) > 2
order by count(*) desc

--group by + функции агрегации
select * from Car
where CarId in (
	select CarId from PurchaseOrder
	group by CarId
	having count(*) >= 2
)

--join
select * from Car c
join PurchaseOrder p on p.CarId = c.CarId

select * from Car c
left join PurchaseOrder p on p.CarId = c.CarId


	
	
