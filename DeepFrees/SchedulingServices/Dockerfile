# Python base image
FROM python:3.9-slim-buster

# Working directory in the container
WORKDIR /App

# Copy requirements.txt file to the container
COPY requirements.txt .

# Python dependencies
RUN pip install --no-cache-dir -r requirements.txt

# Copy Flask app
COPY . .

# Expose port
EXPOSE 5001

# Set environment variable
ENV FLASK_APP=App.py

# Run Flask app
CMD ["flask", "run", "--host", "0.0.0.0"]