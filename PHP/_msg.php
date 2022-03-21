<?php

if (isset($ret)) {
    switch ($ret) {
        case -1:

            echo '<div class="alert alert-danger"> Ocorreu um erro na operação! Tente novamente mais tarde! </div>';

            break;

        case 0;

            echo '<div class="alert alert-warning"> Preencher o(s) campo(s) obrigatório(s)! </div>';

            break;

        case 1;

            echo '<div class="alert alert-success"> Ação realizada com sucesso! </div>';

            break;
        case -2;

            echo '<div class="alert alert-warning"> Não foi possível excluir o registro, pois está sendo utilizado </div>';

            break;
        case -3;
            echo '<div class="alert alert-danger"> Os campos Senha e Repetir Senha não conferem! </div>';
            
            break;
        case -4;
            echo '<div class="alert alert-danger"> Usuário Inexistente! </div>';
            break;
    }
}

