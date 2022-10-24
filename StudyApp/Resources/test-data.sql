BEGIN TRANSACTION;

USE [PracticeQuestions];

CREATE TABLE type (
	type_id INT IDENTITY(1,1),
	type_name VARCHAR(25) NOT NULL,
	CONSTRAINT PK_type PRIMARY KEY (type_id),
	CONSTRAINT QN_type_type_name UNIQUE (type_name)
);

CREATE TABLE question (
	question_id INT IDENTITY(1,1),
	type_id INT,
	text VARCHAR(150) NOT NULL,
	CONSTRAINT PK_question PRIMARY KEY (question_id),
	CONSTRAINT FK_question_type FOREIGN KEY (type_id) REFERENCES type(type_id)
);

CREATE TABLE answer (
	answer_id INT IDENTITY(1,1),
	text VARCHAR(150) NOT NULL,
	CONSTRAINT PK_answer PRIMARY KEY (answer_id)
);

CREATE TABLE question_answer (
	question_answer_id INT IDENTITY(1,1),
	question_id INT NOT NULL,
	answer_id INT NOT NULL,
	is_correct BIT NOT NULL,
	CONSTRAINT PK_question_answer PRIMARY KEY (question_answer_id),
	CONSTRAINT FK_question_answer_question_id FOREIGN KEY (question_id) REFERENCES question(question_id),
	CONSTRAINT FK_question_answer_answer_id FOREIGN KEY (answer_id) REFERENCES answer(answer_id)
);

CREATE TABLE list (
	list_id INT IDENTITY(1,1),
	list_name VARCHAR(50) NOT NULL,
	CONSTRAINT PK_list PRIMARY KEY (list_id)
);

CREATE TABLE question_list (
	question_list_id INT IDENTITY(1,1),
	question_id INT NOT NULL,
	list_id INT NOT NULL,
	CONSTRAINT PK_question_list PRIMARY KEY (question_list_id),
	CONSTRAINT FK_question_list_question_id FOREIGN KEY (question_id) REFERENCES question(question_id),
	CONSTRAINT FK_question_list_list_id FOREIGN KEY (list_id) REFERENCES list(list_id)
);

CREATE TABLE player (
	player_id INT IDENTITY(1,1),
	player_name VARCHAR(20) NOT NULL,
	CONSTRAINT PK_user PRIMARY KEY (player_id),
	CONSTRAINT UQ_user_user_name UNIQUE (player_name)
);

CREATE TABLE highscore (
	highscore_id INT IDENTITY(1,1),
	question_list_id INT NOT NULL,
	player_id INT NOT NULL,
	CONSTRAINT pk_highscore PRIMARY KEY (highscore_id),
	CONSTRAINT FK_highscore_question_list_id FOREIGN KEY (question_list_id) REFERENCES question_list(question_list_id),
	CONSTRAINT FK_highscore_player_id FOREIGN KEY (player_id) REFERENCES player(player_id)
);

INSERT INTO type (type_name) VALUES ('Single Answer'), ('Multiple Answers');

INSERT INTO question (type_id, text) VALUES (2, 'What are the 3 Pillars of Object Oriented Programming?');
INSERT INTO answer (text) VALUES ('Inheritance'), ('Encapsulation'), ('PolyMorphism'), ('Abstraction'), ('...'), ('Something else')
INSERT INTO question_answer (question_id, answer_id, is_correct)
	VALUES
	(1, 1, 1),
	(1, 2, 1),
	(1, 3, 1),
	(1, 4, 1),
	(1, 5, 0),
	(1, 6, 0); -- ID 6

INSERT INTO question (type_id, text) VALUES (1, 'what is a string prefixed with a "$" do?')
INSERT INTO answer (text) VALUES ('Displays the string without having to escape characters.'), ('its what you want to use to concat string togeather..(this should actualy be correct bit theres an answer thats more correct)'), ('string interpolation')
INSERT INTO question_answer (question_id, answer_id, is_correct)
	VALUES
	(2, 7, 0),
	(2, 8, 0),
	(2, 9, 1); -- ID 9

INSERT INTO list (list_name) VALUES ('C# Questions');
INSERT INTO question_list (list_id, question_id) VALUES (1, 1), (1, 2);

COMMIT;