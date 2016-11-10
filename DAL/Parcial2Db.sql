create table Articulos(ArticuloId int primary key identity(1,1),
Descripcion varchar(50),
Existencia int,
Precio decimal
);
go;
create table Ventas(VentaId int primary key identity(1,1),
Fecha date,
Monto decimal
);
go
create table VentasDetalle(Id int primary key identity(1,1),
VentaId int,
ArticuloId int references Articulos(ArticuloId),
Cantidad int,
Precio float
);