package com.rpizzolato.geoquiz

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Button
import android.widget.TextView
import android.widget.Toast

private const val TAG = "MainActivity"

class MainActivity : AppCompatActivity() {

    private lateinit var trueButton: Button
    private lateinit var falseButton: Button
    private lateinit var nextButton: Button
    private lateinit var questionTextView: TextView

    private var score = 0

    private val questionBank = listOf(
        Question(R.string.question_australia, true),
        Question(R.string.question_oceans, true),
        Question(R.string.question_mideast, false),
        Question(R.string.question_americas, false),
        Question(R.string.question_asia, true))

    private var currentIndex = 0

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        Log.d(TAG, "onCreate(Bundle?) called")

        setContentView(R.layout.activity_main)

        trueButton = findViewById(R.id.true_button)
        falseButton = findViewById(R.id.false_button)
        nextButton = findViewById(R.id.next_button)

        questionTextView = findViewById(R.id.question_text_view);

        trueButton.setOnClickListener { view: View ->
            checkAnswer(true)
        }

        falseButton.setOnClickListener { view: View ->
            checkAnswer(false)
        }

        nextButton.setOnClickListener { view: View ->
            nextQuestion()
            checkIfWasAnswered()
            updateQuestion()
        }

        updateQuestion()
    }

    override fun onStart() {
        super.onStart()
        Log.d(TAG, "onStart() called")
    }
    override fun onResume() {
        super.onResume()
        Log.d(TAG, "onResume() called")
    }
    override fun onPause() {
        super.onPause()
        Log.d(TAG, "onPause() called")
    }
    override fun onStop() {
        super.onStop()
        Log.d(TAG, "onStop() called")
    }
    override fun onDestroy() {
        super.onDestroy()
        Log.d(TAG, "onDestroy() called")
    }

    private fun updateQuestion() {
        val questionTextResId = questionBank[currentIndex].textResId
        questionTextView.setText(questionTextResId)
    }

    private fun checkAnswer(userAnswer: Boolean) {
        val correctAnswer = questionBank[currentIndex].answer

        val messageResId = if(userAnswer === correctAnswer) {
            score++
            R.string.correct_toast
        } else {
            R.string.incorrect_toast
        }

        markAnswerAsAnswered()
        checkIfWasAnswered()
        Toast.makeText(this, messageResId, Toast.LENGTH_SHORT).show()

        showScore()
    }

    private fun nextQuestion() {
        currentIndex = (currentIndex + 1) % questionBank.size
    }

    private fun markAnswerAsAnswered() {
        questionBank[currentIndex].wasAnswered = true;
    }

    private fun checkIfWasAnswered() {
        val isQuestionAnswered = questionBank[currentIndex].wasAnswered

        trueButton.isEnabled = !isQuestionAnswered
        falseButton.isEnabled = !isQuestionAnswered
    }

    private fun showScore() {
        questionBank.forEach{
            if (!it.wasAnswered)
                return
        }

        val percentage = score * 100 / questionBank.size
        val message = "You scored $percentage% of the questions!"

        Toast.makeText(this, message, Toast.LENGTH_LONG).show()
    }
}