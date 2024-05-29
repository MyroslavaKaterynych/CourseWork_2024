var alphabet = ['A','Б','В','Г','Д','Е','Є','Ж'];


function create_test()
{
    test_count++;
    test_count++;
    var select_id = "CreateTest_select_" + test_count;
    var div = document.createElement("div");
    var select = document.createElement("select");
    select.id = select_id;
    select.onchange = function () {
        selectTypeTest(select_id);
    };
    var option = document.createElement("option");
    option.text = "Виберіть тип питання";
    select.appendChild(option);
    var option1 = document.createElement("option");
    option1.text = "Звичайний тест";
    select.appendChild(option1);
    var option2 = document.createElement("option");
    option2.text = "З відкритою відповідю";
    select.appendChild(option2);
    var option3 = document.createElement("option");
    option3.text = "З декількома вірними відповідями";
    select.appendChild(option3);
    var option4 = document.createElement("option");
    option4.text = "З встановленням відповідностей";
    select.appendChild(option4);
    var option5 = document.createElement("option");
    option5.text = "З встановленням послідовності";
    select.appendChild(option5);

    // Додати інші елементи option, які вам потрібні
    div.appendChild(select);
    var insertedTestDiv = document.createElement("div");
    insertedTestDiv.id = "DIV_" + select_id;
    insertedTestDiv.className = "insertedTest";
    div.appendChild(insertedTestDiv);
    document.getElementById("test").appendChild(div);
}

function selectTypeTest( id_select)
{
    var val = document.getElementById(id_select).value;
    var id_test = document.getElementById('id_test').innerHTML;
   // alert(val);
    switch (val) {
        case "З відкритою відповідю":
            $.ajax({
                type: "GET",
                url: 'PartialOpenQuestion',
                data: ({ id_test: id_test }),
                success: function (html) {
                    document.getElementById('DIV_' + id_select).innerHTML = html;
                }
            });
            break;
        case "З встановленням відповідностей":
            $.ajax({
                type: "GET",
                url: 'PartialCompliance',
                data: ({ id_test: id_test }),
                success: function (html) {
                    document.getElementById('DIV_' + id_select).innerHTML = html;
                }
            });
            break;
        case "З встановленням послідовності":
            $.ajax({
                type: "GET",
                url: 'PartialSuquence',
                data: ({ id_test: id_test }),
                success: function (html) {
                    document.getElementById('DIV_' + id_select).innerHTML = html;
                }
            });
            break;
        case "Звичайний тест":
            $.ajax({
                type: "GET",
                url: 'PartialTest',
                data: ({ id_test: id_test }),
                success: function (html) {
                    document.getElementById('DIV_' + id_select).innerHTML = html;
                }
            });
            break;
        case "З декількома вірними відповідями":
            $.ajax({
                type: "GET",
                url: 'PartialMultipleOption',
                data: ({ id_test: id_test }),
                success: function (html) {
                    document.getElementById('DIV_' + id_select).innerHTML = html;
                }
            });
            break;
    }
}
function DeleteTest(id, table_name) {
    event.preventDefault();
    $.ajax({
        type: "POST",
        url: 'DeleteTest',
        data: ({ id_test: id, table_name: table_name }),
        success: function (result) {
            console.log(result);
            if (result == true) {
                var _div = document.getElementById(id);
                var form = _div.closest('form');
                var div1 = form.closest('div');
                var div2 = div1.parentElement;
                if (div2) {
                    console.log(div2);
                    div2.remove(); // Видалити батьківський тег form
                } else {
                  
                }
            }
        }
        });
}
function PartialComplianceAddLineNumbers(textareaId, numberingStyle) {
    var textarea = document.getElementById(textareaId);
    if (numberingStyle == "numeric") {
        var znach = textarea.value;
        if (znach[znach.length - 1] === "\n") {
            console.log("Perenos strki");
            var selectionStart = textarea.selectionStart;
            var textBeforeCursor = textarea.value.substring(0, selectionStart);
            var lineNumber = textBeforeCursor.split('\n').length;
            console.log("NUMBER: " + lineNumber);
            textarea.value += "" + lineNumber + ". ";
        } else {
            console.log("ne perenos stroki");
        }
    }
    else if (numberingStyle == "alphabetic")

    {
        var znach = textarea.value;
        if (znach[znach.length - 1] === "\n") {
            console.log("Perenos strki");
            var selectionStart = textarea.selectionStart;
            var textBeforeCursor = textarea.value.substring(0, selectionStart);
            var lineNumber = textBeforeCursor.split('\n').length;
            console.log("Alphabet: " + lineNumber);
            textarea.value += "" +alphabet [ lineNumber-1 ]+ ") ";
        } else {
            console.log("ne perenos stroki");
        }
    }
}
function CheckTest() {
    var id_test = document.getElementById('test').innerText.trim();
    var test_block = document.getElementsByClassName('PassTest_Test');
    var jsonData = [];

    for (let i = 0; i < test_block.length; i++) {
        var testType = test_block[i].children[0].innerText.trim();
        var questionText = test_block[i].querySelector('.PassTest_div_question').innerText.trim();
        var answerInput = test_block[i].querySelector('.PassTest_input_answer');
        var answer = answerInput ? answerInput.value.trim() : '';

        let answerText = "";

        if (testType !== "OpenQuestion") {
            if (answer) {
                let options = test_block[i].querySelectorAll('.answer-option');
                answer.split(',').forEach(letter => {
                    letter = letter.trim();
                    options.forEach(option => {
                        if (option.querySelector('.option-letter').innerText.trim().startsWith(letter)) {
                            answerText += (answerText ? ", " : "") + option.querySelector('.option-text').innerText.trim();
                        }
                    });
                });
            }
        } else {
            answerText = answerInput.value;
        }

        if (["Test", "MultipleOption", "Suquence", "OpenQuestion"].includes(testType)) {
            jsonData.push({
                type: testType,
                question: questionText,
                answer: answerText,
                id_test: id_test
            });
        } else if (testType === "Compliance") {
            var complianceData = {
                type: testType,
                question: questionText,
                id_test: id_test,
                questions: []
            };

            var complianceQuestions = test_block[i].querySelectorAll('.PassTest_Compliance_div_variant_question');
            var complianceAnswers = test_block[i].querySelectorAll('.PassTest_Compliance_input_variant_answer');

            for (let j = 0; j < complianceQuestions.length; j++) {
                var complianceQuestionText = complianceQuestions[j].innerText.trim();
                var complianceAnswerText = complianceAnswers[j].value.trim().toUpperCase();

                let answerText = "";
                if (complianceAnswerText) {
                    let options = test_block[i].querySelectorAll('.answer-option');
                    options.forEach(option => {
                        if (option.querySelector('.option-letter').innerText.trim().startsWith(complianceAnswerText)) {
                            answerText = option.querySelector('.option-text').innerText.trim();
                        }
                    });
                }

                complianceData.questions.push({
                    question: complianceQuestionText,
                    answer: answerText
                });
            }

            jsonData.push(complianceData);
        }
    }

    console.log(jsonData);
    var jsonString = JSON.stringify(jsonData, null, 2);
    console.log(jsonString);

    $.ajax({
        url: 'CheckThisTask',
        type: 'GET',
        data: { str: jsonString },
        success: function (result) {
            alert("Ви набрали: "+result+" балів");
        }
    });
}


//function CheckTest() {
//    var id_test = document.getElementById('test').innerHTML;
//    var test_block = document.getElementsByClassName('PassTest_Test');
//    var jsonData = [];

//    for (let i = 0; i < test_block.length; i++) {
//        var testType = test_block[i].children[0].innerText;
//        var questionText = test_block[i].querySelector('.PassTest_div_question').innerText;
//        var answerInput = test_block[i].querySelector('.PassTest_input_answer');

//        if (testType === "Test" || testType === "MultipleOption" || testType === "Suquence" || testType === "OpenQuestion") {
//            var answer = answerInput.value;
//            jsonData.push({
//                type: testType,
//                question: questionText,
//                answer: answer, id_test: id_test
//            });
//        } else if (testType === "Compliance") {
//            var complianceData = {
//                type: testType,
//                question: questionText, id_test: id_test,
//                questions: []
//            };

//            var complianceQuestions = test_block[i].querySelectorAll('.PassTest_Compliance_div_variant_question');
//            var complianceAnswers = test_block[i].querySelectorAll('.PassTest_Compliance_input_variant_answer');

//            for (let j = 0; j < complianceQuestions.length; j++) {
//                var complianceQuestionText = complianceQuestions[j].innerText;
//                var complianceAnswerText = complianceAnswers[j].value;
//                complianceData.questions.push({
//                    question: complianceQuestionText,
//                    answer: complianceAnswerText
//                });
//            }

//            jsonData.push(complianceData);
//        }
//    }

//    // Now jsonData contains the structured data, ready to be sent via AJAX
//    console.log(jsonData);
//    var jsonString = JSON.stringify(jsonData, null, 2);
//    console.log(jsonString);
//    //// Example AJAX call (assuming you are using jQuery)
//    $.ajax({
//        url: 'CheckThisTask',
//        type: 'GET',
//        data: ({ str: jsonString }),
//        success: function (result) {

//        }
       
//    });
//}



