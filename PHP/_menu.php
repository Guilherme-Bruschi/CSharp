<?php

require_once '../dao/UtilDAO.php';

if(isset($_GET['close']) && $_GET['close'] == '1'){
    UtilDAO::Deslogar();
}


?>

<nav class="navbar-default navbar-side" role="navigation">
    <div class="sidebar-collapse">
        <ul class="nav" id="main-menu">
            


            <li>
                <a  href="gerenciar_categoria.php"><i class="fa fa-edit fa-3x"></i> Categoria</a>
            </li>
            <li>
                <a  href="gerenciar_conta.php"><i class="fa fa-desktop fa-3x"></i> Conta</a>
            </li>
            <li>
                <a  href="gerenciar_empresa.php"><i class="fa fa-archive fa-3x"></i> Empresa</a>
            </li>
            <li  >
                <a  href="realizar_movimento.php"><i class="fa fa-money fa-3x"></i> Realizar Movimentos</a>
            </li>	
            <li  >
                <a  href="pesquisar_movimento.php"><i class="fa fa-search fa-3x"></i> Pesquisar Movimentos</a>
            </li>
            <li  >
                <a  href="_menu.php?close=1"><i class="fa fa-ban fa-3x"></i> Sair</a>
            </li>

        </ul>

    </div>

</nav>  
