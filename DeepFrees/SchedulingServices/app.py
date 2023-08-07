from flask import Flask, request, jsonify
import json

app = Flask(__name__)

def schedule_employees(employees, tasks):
    schedule = {}

    for task in tasks:
        best_employee = None
        best_score = float('inf')

        for employee in employees:
            if employee['availability'] >= task['duration']:
                score = employee['workload']
                if score < best_score:
                    best_score = score
                    best_employee = employee

        if best_employee:
            if best_employee['workload'] + task['duration'] <= best_employee['availability']:
                best_employee['workload'] += task['duration']
                if best_employee['id'] not in schedule:
                    schedule[best_employee['id']] = []
                schedule[best_employee['id']].append(task['id'])

    return schedule

@app.route('/schedule', methods=['POST'])
def generate_schedule():
    data = request.json

    employees = data.get('employees', [])
    tasks = data.get('tasks', [])

    schedule = schedule_employees(employees, tasks)

    return jsonify(schedule)

if __name__ == '__main__':
    app.run(debug=True)