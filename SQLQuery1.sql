Create TABLE users (
id INT NOT NULL PRIMARY KEY IDENTITY,
type char (10) NOT NULL DEFAULT Client,
name varchar (100) NOT NULL,
email VARCHAR (150) NOT NULL UNIQUE,
phone VARCHAR(20) NULL,
pass char NOT NULL,
created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP 
);

INSERT INTO users (name,type,email,phone,pass)
VALUES
('root','Admin','admin@admin.admin', 'test','adminx'),
('Mod1','Mod','mod@mod.mod','0554878','modx'),
('Client1','Client','client@client.client','00619841','clientx');
