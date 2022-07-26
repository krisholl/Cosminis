create schema WALS_P2;

create table WALS_P2.users(
		userId int identity,
		username varchar(30) unique not null,
		password varchar(30) not null,
		goldCount int,
  		eggCount int,
  		eggTimer int,
  		primary key (userId)
 		);

create table WALS_P2.companions(
		creatureId int identity,
		user_fk int not null foreign key references WALS_P2.users(userId),
		species_fk int not null foreign key references WALS_P2.species(speciesId),
		nickname varchar(30),
		mood varchar(8) not null check (mood in ('Happy', 'Sad', 'Angry', 'Tired', 'Anxious', 'Excited', 'Chill')),
		hunger int,
		primary key (creatureId)
		);
	
create table WALS_P2.species(
		speciesId int identity,
		foodElement_fk int not null foreign key references WALS_P2.foodElement(foodElementId),
		speciesName Varchar not null,
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