from flask import Flask, jsonify
from flask_caching import Cache
import mysql.connector

# Create Flask app instance
app = Flask(__name__)

#define cahce
app.config['CACHE_TYPE'] = 'simple'  # Use in-memory caching
app.config['CACHE_DEFAULT_TIMEOUT'] = 300  # Set cache timeout to 300 seconds (5 minutes)

cache = Cache(app)

# Define a route and its corresponding view function
@app.route('/Schedule/<int:id>')
@cache.cached()
def get_data(id):
    #cache check if exist
    cached_data = cache.get(str(id))
    if cached_data:
        return cached_data
    



    # Return the data as JSON
    return jsonify(data)

if __name__ == '__main__':
    import os
    HOST = os.environ.get('SERVER_HOST', 'localhost')
    try:
        PORT = int(os.environ.get('SERVER_PORT', '5555'))
    except ValueError:
        PORT = 5555
    app.run(HOST, PORT)

