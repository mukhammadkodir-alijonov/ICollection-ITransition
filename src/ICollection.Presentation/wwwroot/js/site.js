// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// for dark mode
function myFunction() {
    var element = document.body;
    element.classList.toggle("dark-mode");
}


/*for like*/function toggleLikeDislike(collectionId, element) {
    // Check if the element has 'liked' class
    if (element.classList.contains('liked')) {
        // Dislike action
        fetch(`/collections/dislikecollection?collectionId=${collectionId}`, {
            method: 'GET', // or 'GET' based on your server-side implementation
            headers: {
                'Content-Type': 'application/json',
                // You may need additional headers based on your server configuration
            },
        })
            .then(response => {
                if (response.ok) {
                    // Remove 'liked' class, change icon to outline heart
                    element.classList.remove('liked');
                    element.classList.remove('fa-heart-solid');
                    element.classList.add('fa-heart');
                } else {
                    // Handle error response here
                    console.error('Failed to dislike collection');
                }
            })
            .catch(error => {
                console.error('Error:', error);
            });
    } else {
        // Like action
        fetch(`/collections/likecollection?collectionId=${collectionId}`, {
            method: 'GET', // or 'GET' based on your server-side implementation
            headers: {
                'Content-Type': 'application/json',
                // You may need additional headers based on your server configuration
            },
        })
            .then(response => {
                if (response.ok) {
                    // Add 'liked' class, change icon to solid heart
                    element.classList.add('liked');
                    element.classList.remove('fa-heart');
                    element.classList.add('fa-heart-solid');
                } else {
                    // Handle error response here
                    console.error('Failed to like collection');
                }
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }
}
