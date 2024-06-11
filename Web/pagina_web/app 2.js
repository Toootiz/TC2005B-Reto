
//Esta parte es para que funcione el gráfico 
//(cuando tengamos de donde sacar los datos)

/*
document.addEventListener('DOMContentLoaded', function() {
    fetch('http://localhost:3000/api/cards') // La URL de tu API para obtener todas las cartas
        .then(response => response.json())
        .then(data => {
            const labels = data.map(item => item.nombre);
            const healthPoints = data.map(item => item.puntos_de_vida);
            const attackPoints = data.map(item => item.puntos_de_ataque);

            const ctx = document.getElementById('statsChart').getContext('2d');
            const statsChart = new Chart(ctx, {
                type: 'bar', // Puedes cambiar esto por 'line' o 'radar' para diferentes tipos de gráficos
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Puntos de Vida',
                        data: healthPoints,
                        backgroundColor: 'rgba(75, 192, 192, 0.2)',
                        borderColor: 'rgba(75, 192, 192, 1)',
                        borderWidth: 1
                    },
                    {
                        label: 'Puntos de Ataque',
                        data: attackPoints,
                        backgroundColor: 'rgba(255, 99, 132, 0.2)',
                        borderColor: 'rgba(255, 99, 132, 1)',
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        })
        .catch(error => console.error('Error al cargar los datos:', error));
});
*/