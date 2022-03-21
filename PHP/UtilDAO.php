<?php

class UtilDAO {

    private static function IniciarSessao() {
        if (!isset($_SESSION)) {
            session_start();
        }
    }

    public static function CriarSessao($cod) {
        self::IniciarSessao();
        $_SESSION['cod'] = $cod;
    }

    public static function CodigoLogado() {
        self::IniciarSessao();
        return $_SESSION['cod'];
    }

    public static function Deslogar() {
        self::IniciarSessao();
        unset($_SESSION['cod']);
        header('location: login.php');
        exit;
    }

    public static function VerificarLogado() {
        self::IniciarSessao();
        if (!isset($_SESSION['cod']) || $_SESSION['cod'] == '') {
            header('location: login.php');
            exit;
        }
    }

}
