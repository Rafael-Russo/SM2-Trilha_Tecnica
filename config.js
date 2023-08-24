(function() {
    const $fileList = document.getElementById('file-list');
    const repoOwner = 'Rafael-Russo';
    const repoName = 'SM2-Trilha_Tecnica';

    fetch(`https://api.github.com/repos/${repoOwner}/${repoName}/contents`)
        .then(response => response.json())
        .then(data => {
            data.forEach(item => {
                if (item.type === "file") {
                    const listItem = document.createElement("li");
                    const link = document.createElement("a");
                    link.href = item.html_url;
                    link.textContent = item.name;
                    listItem.appendChild(link);

                    $fileList.appendChild(listItem);
                }
            });
        })
        .catch(error => {
            console.error("Erro ao obter a lista de arquivos:", error);
        });
})()