create schema WALS_P2;

create table WALS_P2.users(
		userId int identity,
		username varchar(30) unique not null,
		password varchar(30) not null,
		account_age datetime not null,
		goldCount int,
  		eggCount int,
  		eggTimer datetime not null,
  		primary key (userId)
 		);

create table WALS_P2.companions(
		companionId int identity,
		user_fk int not null foreign key references WALS_P2.users(userId),
		species_fk int not null foreign key references WALS_P2.species(speciesId),
		nickname varchar(30),
		mood varchar(8) not null check (mood in ('Happy', 'Sad', 'Angry', 'Tired', 'Anxious', 'Excited', 'Chill')),
		hunger int,
		companion_birthday datetime,
		primary key (companionId)
		);
	
create table WALS_P2.species(
		speciesId int identity,
		foodElementId_fk int not null foreign key references WALS_P2.foodElement(foodElementId),
		speciesName varchar(25) not null,
		description varchar(255) not null,
		baseStr int,
		baseDex int,
		baseInt int,
		elementType varchar(8) not null check (elementType in ('Volcanic', 'Glacial', 'Forest', 'Sky', 'Holy', 'Dark')),
		primary key (speciesId)
		);
	
	
create table WALS_P2.foodElement(
		foodElementId int identity,
		foodElement varchar(8) not null check (foodElement in ('Spicy', 'Cold', 'Leafy', 'Fluffy', 'Blessed', 'Cursed')),
		primary key (foodElementId)
		);
		
create table WALS_P2.foodStats(
		foodStatsId int identity,
		foodElement_fk int not null foreign key references WALS_P2.foodElement(foodElementId),
		description varchar(255),
		foodName varchar(45) not null,
		hungerRestore int not null,
		primary key (foodStatsId)
		);
	
create table WALS_P2.foodInventory(
		userId_fk int not null foreign key references WALS_P2.users(userId),
		foodStatsId_fk int not null foreign key references WALS_P2.foodStats(foodStatsId),
		foodCount int,
		primary key (userId_fk, foodStatsId_fk)
		);
	
create table WALS_P2.friends(
		userId_fk1 int not null foreign key references WALS_P2.users(userId),
		userId_fk2 int not null foreign key references WALS_P2.users(userId),
		primary key (userId_fk1, userId_fk2)
		);
	
create table WALS_P2.posts(
		postId int identity,
		userId_fk int not null foreign key references WALS_P2.users(userId),
		content varchar(255) not null,
		primary key (postId)
		);
		
create table WALS_P2.likes(
		userId_fk int not null foreign key references WALS_P2.users(userId),
		postId_fk int not null foreign key references WALS_P2.posts(postId),
		primary key (userId_fk, postId_fk)
		);
	
create table WALS_P2.comments(
		commentId int identity,
		userId_fk int not null foreign key references WALS_P2.users(userId),
		postId_fk int not null foreign key references WALS_P2.posts(postId),
		content varchar(255) not null,
		primary key (commentId)
		);
	
insert into WALS_P2.foodStats (foodElement_fk, description, foodName, hungerRestore) values (6,'Wear floaties.', 'Devil Fruit', 50);		

insert into WALS_P2.foodInventory;

select * from WALS_P2.foodStats;

drop table WALS_P2.species;

insert into WALS_P2.species (foodElementId_fk, speciesName, description, baseStr, baseDex, baseInt, elementType) values (6,'Cancer', 'Blursed friend.', 10, 10, 10, 'Dark');

delete from WALS_P2.foodStats where foodStatsId = 4;