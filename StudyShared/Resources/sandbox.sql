SELECT question.text, type.type_name FROM question
JOIN question_list ON question.question_id = question_list.question_id
JOIN type ON type.type_id = question.type_id
WHERE question_list.list_id = 1

SELECT answer.answer_id, text, is_correct FROM answer JOIN question_answer ON question_answer.answer_id = answer.answer_id WHERE question_answer.question_id = 1

SELECT TOP 1 question_list_id, list.list_name FROM question_list 
JOIN list ON question_list.list_id = list.list_id 
WHERE question_list.list_id = 1;

SELECT list_name, list.list_id FROM list
WHERE list_id = 1


SELECT question.question_id, question.text, type.type_name FROM question JOIN question_list ON question.question_id = question_list.question_id AND question_list.list_id = 1 JOIN type ON type.type_id = question.type_id

SELECT question.question_id, type.type_name, question.text FROM list 
JOIN question_list ON question_list.list_id = 1 
JOIN question ON question.question_id = question_list.question_id 
JOIN type ON type.type_id = question.type_id 
WHERE list.list_id = 1;