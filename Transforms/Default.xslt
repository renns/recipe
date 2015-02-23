<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/recipe">
  <html>
  <head>
    <style>
      body {
        font-family: "Helvetica Neue",helvetica,arial;
      }
      div.source, div.description {
        margin-bottom: 20px;
      }
      span.label {
        font-weight: bold;
      }
    </style>
  </head>
  <body>
    <h1><xsl:value-of select="title"/></h1>
    <xsl:if test="(author and author != '') or (sourceUri and sourceUri != '')">
      <div class="source">
        <xsl:if test="author and author != ''">
          <div class="author">
            <span class="label">Author: </span>
            <xsl:value-of select="author"/>
          </div>
        </xsl:if>
        <xsl:if test="sourceUri and sourceUri != ''">
          <a>
            <xsl:attribute name="href">
              <xsl:value-of select="sourceUri"/>
            </xsl:attribute>
            <xsl:value-of select="sourceUri"/>
          </a>
        </xsl:if>
      </div>
    </xsl:if>
    <xsl:if test="description and description != ''">
      <div class="description">
        <xsl:value-of select="description"/>
      </div>
    </xsl:if>
    <xsl:if test="yield and yield != ''">
      <div class="yield">
        <span class="label">Yield: </span>
        <xsl:value-of select="yield"/>
      </div>
    </xsl:if>
    <xsl:if test="preparationTime and preparationTime != ''">
      <div class="preparationTime">
        <span class="label">Preparation Time: </span>
        <xsl:value-of select="preparationTime"/>
      </div>
    </xsl:if>
    <xsl:if test="cookingTime and cookingTime != ''">
      <div class="cookingTime">
        <span class="label">Cooking Time: </span>
        <xsl:value-of select="cookingTime"/>
      </div>
    </xsl:if>
    <xsl:if test="totalTime and totalTime != ''">
      <div class="totalTime">
        <span class="label">Total Time: </span>
        <xsl:value-of select="totalTime"/>
      </div>
    </xsl:if>
    <xsl:if test="ingredientSections/ingredientSection">
      <h2>Ingredients</h2>
      <xsl:for-each select="ingredientSections/ingredientSection">
        <xsl:if test="@heading and @heading != ''">
          <h3>
            <xsl:value-of select="@heading"/>
          </h3>
        </xsl:if>
        <ul>
          <xsl:for-each select="ingredients/ingredient">
            <li>
              <xsl:value-of select="."/>
            </li>
          </xsl:for-each>
        </ul>
      </xsl:for-each>
    </xsl:if>
    <xsl:if test="stepSections/stepSection">
      <h2>Directions</h2>
      <xsl:for-each select="stepSections/stepSection">
        <xsl:if test="@heading and @heading != ''">
          <h3>
            <xsl:value-of select="@heading"/>
          </h3>
        </xsl:if>
        <ol>
          <xsl:for-each select="steps/step">
            <li>
              <xsl:value-of select="."/>
            </li>
          </xsl:for-each>
        </ol>
      </xsl:for-each>
    </xsl:if>
    <xsl:if test="notes/note">
      <h2>Notes</h2>
      <ul>
        <xsl:for-each select="notes/note">
          <li>
            <xsl:value-of select="."/>
          </li>
        </xsl:for-each>
      </ul>
    </xsl:if>
  </body>
  </html>
</xsl:template>
  
</xsl:stylesheet>
