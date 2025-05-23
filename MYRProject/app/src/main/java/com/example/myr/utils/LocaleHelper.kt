package com.example.myr.utils

import android.content.Context
import android.content.res.Configuration
import android.preference.PreferenceManager
import java.util.*

object LocaleHelper {

    private const val SELECTED_LANGUAGE = "Locale.Helper.Selected.Language"

    fun setLocale(context: Context, language: String): Context {
        persist(context, language)
        return updateResources(context, language)
    }

    fun applySavedLocale(context: Context): Context {
        val preferences = PreferenceManager.getDefaultSharedPreferences(context)
        val language = preferences.getString(SELECTED_LANGUAGE, "en") ?: "en"
        return updateResources(context, language)
    }

    private fun persist(context: Context, language: String) {
        val preferences = PreferenceManager.getDefaultSharedPreferences(context)
        preferences.edit().putString(SELECTED_LANGUAGE, language).apply()
    }

    private fun updateResources(context: Context, language: String): Context {
        val locale = Locale(language)
        Locale.setDefault(locale)

        val config = Configuration(context.resources.configuration)
        config.setLocale(locale)

        return context.createConfigurationContext(config)
    }
}
