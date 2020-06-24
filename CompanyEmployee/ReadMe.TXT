1. Создать БД с именем Lesson7

2. Создать таблицу EmployeeTable
CREATE TABLE [dbo].[EmployeeTable] (
    [Id]         INT             IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (MAX)  NOT NULL,
    [Surname]    NVARCHAR (MAX)  NOT NULL,
    [Age]        TINYINT         NOT NULL,
    [Salary]     NUMERIC (18, 2) NOT NULL,
    [Department] INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EmployeeTable_DepartmentTable] FOREIGN KEY ([Department]) REFERENCES [dbo].[DepartmentTable] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
);

3. Для заполнения текстовыми данными выполнить
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Степан', N'Рыбаков', '25', '35000', N'Маркетинг');
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Светлана', N'Потапова', '31', '40000', N'Маркетинг');
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Мария', N'Лапина', '36', '28000', N'Сбыт');
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Екатерина', N'Боброва', '27', '52000', N'Инновации');
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Алексей', N'Зуев', '48', '48000', N'Закупки');
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Дмитрий', N'Панов', '29', '62000', N'Закупки');
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Анастасия', N'Журавлева', '19', '40000', N'Финансы');
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Николай', N'Орехов', '20', '36000', N'Маркетинг');
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Петр', N'Жуков', '21', '21000', N'Связи с общественностью');
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Василий', N'Лазарев', '22', '35000', N'Финансы');
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Артем', N'Емельянов', '23', '29000', N'Связи с общественностью');
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Владимир', N'Косых', '29', '50000', N'Администрация');
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Алексей', N'Захаров', '33', '55000', N'Сбыт');
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Александр', N'Слепаков', '36', '40000', N'Администрация');
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Михаил', N'Щукин', '38', '40000', N'Кадры');
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Петр', N'Крыжовников', '40', '62000', N'Инновации');
Insert Into EmployeeTable (Name, Surname, Age, Salary, Department) Values (N'Сергей', N'Малинин', '42', '50000', N'Инновации');

4. Создать таблицу DepartmentTable
CREATE TABLE [dbo].[DepartmentTable] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [name] NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

5. Для заполнения текстовыми данными выполнить
Insert Into DepartmentTable (Name) Values (N'Маркетинг');
Insert Into DepartmentTable (Name) Values (N'Сбыт');
Insert Into DepartmentTable (Name) Values (N'Инновации');
Insert Into DepartmentTable (Name) Values (N'Закупки');
Insert Into DepartmentTable (Name) Values (N'Финансы');
Insert Into DepartmentTable (Name) Values (N'Связи с общественностью');
Insert Into DepartmentTable (Name) Values (N'Кадры');
Insert Into DepartmentTable (Name) Values (N'Администрация');

