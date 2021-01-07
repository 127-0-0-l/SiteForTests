class Test {
    TestName
    Questions = []
}

class Question {
    QuestionText
    RightAnswerId
    Answers = []
}

class Answer {
    constructor(id, answerText) {
        this.Id = id
        this.AnswerText = answerText
    }
}

var questionCount = 0
var answersCount = new Map()

function AddQuestion() {
    questionCount++
    answersCount.set(questionCount, 0)

    var div = document.createElement('div')
    div.id = 'question' + questionCount
    div.innerHTML =
        questionCount + '. question' +
        '<input id=questionText' + questionCount + ' type=text/><br />' +
        'right answer' +
        '<input id=rightAnswerId' + questionCount + ' type=number><br />' +
        '<button onclick=AddAnswer(' + questionCount + ')>Add answer</button>'

    btnCreate.before(div)
}

function AddAnswer(questionId) {
    var answer = document.createElement('div')
    answer.id = 'answer' + questionId
    answersCount.set(questionId, answersCount.get(questionId) + 1)

    var answerNum = answersCount.get(questionId)
    var innerString = '. answer<input id=' + questionId + 'answerText' + answerNum + ' type="text" />'

    answer.innerHTML = answerNum + innerString

    var element = document.getElementById('question' + questionId)
    element.append(answer)
}

function CreateTest() {
    var test = new Test();
    test.TestName = document.getElementById("testNameText").value

    for (var i = 1; document.getElementById('questionText' + i); i++) {
        var newQuestion = new Question()

        newQuestion.QuestionText = document.getElementById('questionText' + i).value
        newQuestion.RightAnswerId = document.getElementById('rightAnswerId' + i).value

        for (var j = 1; document.getElementById(i + 'answerText' + j) != null; j++) {
            var text = document.getElementById(i + 'answerText' + j).value
            newQuestion.Answers.push(new Answer(j, text))
        }
        test.Questions.push(newQuestion)
    }

    var jsonString = JSON.stringify(test)

    LoadJsonData(jsonString)
}